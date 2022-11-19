namespace Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.Json;
	using System.Threading.Tasks;
	using Fluxera.Entity;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;
	using MassTransit;
	using ObjectDelta;

	/// <summary>
	///     A base class for updated domain event handlers.
	/// </summary>
	/// <typeparam name="TAggregateRoot"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TDomainEvent"></typeparam>
	/// <typeparam name="TEvent"></typeparam>
	[PublicAPI]
	public abstract class ItemUpdatedEventHandlerBase<TAggregateRoot, TKey, TDomainEvent, TEvent> : DomainEventHandler<TDomainEvent>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
		where TKey : IComparable<TKey>, IEquatable<TKey>
		where TDomainEvent : ItemUpdated<TAggregateRoot, TKey>
		where TEvent : ItemUpdated, new()
	{
		private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;
		private readonly IPublishEndpoint publishEndpoint;

		/// <inheritdoc />
		protected ItemUpdatedEventHandlerBase(IPublishEndpoint publishEndpoint, IDateTimeOffsetProvider dateTimeOffsetProvider)
		{
			this.dateTimeOffsetProvider = dateTimeOffsetProvider;
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public sealed override async Task HandleAsync(TDomainEvent domainEvent)
		{
			// The event name must start with the entity name.
			string eventName = typeof(TEvent).Name;
			string entityName = typeof(TAggregateRoot).Name;
			string entityLongName = typeof(TAggregateRoot).FullName;
			if(!eventName.StartsWith(entityName))
			{
				throw new InvalidOperationException(
					"The event name of an updated event handler must start with the matching entity name.");
			}

			TAggregateRoot before = domainEvent.BeforeUpdateItem;
			TAggregateRoot after = domainEvent.AfterUpdateItem;

			ObjectDelta<TAggregateRoot> delta = before.CompareTo(after);
			IList<Change> changes = delta.PropertyDeltas.Select(x => new Change
			{
				Path = x.Path,
				BeforeUpdateValue = x.OldValue,
				AfterUpdateValue = x.NewValue,
				DataType = x.Type
			}).ToList();

			TAggregateRoot item = after;

			// ReSharper disable once SuspiciousTypeConversion.Global
			IAuditedObject auditedObject = after as IAuditedObject;

			TEvent message = new TEvent
			{
				Metadata = new ItemUpdatedData
				{
					EntityID = item.ID.ToString(),
					EntityName = entityName,
					EntityLongName = entityLongName,
					LastModifiedAt = auditedObject?.LastModifiedAt ?? this.dateTimeOffsetProvider.UtcNow,
					LastModifiedBy = auditedObject?.LastModifiedBy,
					BeforeUpdateState = JsonSerializer.Serialize(before),
					AfterUpdateState = JsonSerializer.Serialize(after),
					Changes = changes
				},
			};

			await this.InitializeAsync(message, item);

			// Publish the event message on the message bus.
			await this.publishEndpoint.Publish(message);
		}

		/// <summary>
		///     Initialize the the event message from the underlying item.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		protected virtual Task InitializeAsync(TEvent message, TAggregateRoot item)
		{
			return Task.CompletedTask;
		}
	}
}
