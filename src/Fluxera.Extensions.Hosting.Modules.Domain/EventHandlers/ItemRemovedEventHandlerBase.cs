namespace Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers
{
	using System;
	using System.Text.Json;
	using System.Threading.Tasks;
	using Fluxera.Entity;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A base class for removed domain event handlers.
	/// </summary>
	/// <typeparam name="TAggregateRoot"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TDomainEvent"></typeparam>
	/// <typeparam name="TEvent"></typeparam>
	[PublicAPI]
	public abstract class ItemRemovedEventHandlerBase<TAggregateRoot, TKey, TDomainEvent, TEvent> : DomainEventHandler<TDomainEvent>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
		where TKey : IComparable<TKey>, IEquatable<TKey>
		where TDomainEvent : ItemRemoved<TAggregateRoot, TKey>
		where TEvent : ItemRemoved, new()
	{
		private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;
		private readonly IPublishEndpoint publishEndpoint;

		/// <inheritdoc />
		protected ItemRemovedEventHandlerBase(IPublishEndpoint publishEndpoint, IDateTimeOffsetProvider dateTimeOffsetProvider)
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
					"The event name of a removed event handler must start with the matching entity name.");
			}

			TAggregateRoot item = domainEvent.RemovedItem;

			// ReSharper disable once SuspiciousTypeConversion.Global
			IAuditedObject auditedObject = item as IAuditedObject;

			TEvent message = new TEvent
			{
				Metadata = new ItemRemovedData
				{
					EntityID = domainEvent.ID.ToString(),
					EntityName = entityName,
					EntityLongName = entityLongName,
					DeletedAt = auditedObject?.DeletedAt ?? this.dateTimeOffsetProvider.UtcNow,
					DeletedBy = auditedObject?.DeletedBy,
					BeforeDeletedState = JsonSerializer.Serialize(item)
				}
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
