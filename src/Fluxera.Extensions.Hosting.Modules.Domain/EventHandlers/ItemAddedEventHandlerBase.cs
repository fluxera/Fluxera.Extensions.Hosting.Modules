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
	///     A base class for added domain event handlers.
	/// </summary>
	/// <typeparam name="TAggregateRoot"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TDomainEvent"></typeparam>
	/// <typeparam name="TEvent"></typeparam>
	[PublicAPI]
	public abstract class ItemAddedEventHandlerBase<TAggregateRoot, TKey, TDomainEvent, TEvent> : DomainEventHandler<TDomainEvent>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>
		where TKey : IComparable<TKey>, IEquatable<TKey>
		where TDomainEvent : ItemAdded<TAggregateRoot, TKey>
		where TEvent : ItemAdded, new()
	{
		private readonly IDateTimeOffsetProvider dateTimeOffsetProvider;
		private readonly IPublishEndpoint publishEndpoint;

		/// <inheritdoc />
		protected ItemAddedEventHandlerBase(IPublishEndpoint publishEndpoint, IDateTimeOffsetProvider dateTimeOffsetProvider)
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
					"The event name of an Added event handler must start with the matching entity name.");
			}

			TAggregateRoot item = domainEvent.AddedItem;

			// ReSharper disable once SuspiciousTypeConversion.Global
			IAuditedObject auditedObject = item as IAuditedObject;

			TEvent message = new TEvent
			{
				Metadata = new ItemAddedData
				{
					EntityID = item.ID.ToString(),
					EntityName = entityName,
					EntityLongName = entityLongName,
					CreatedAt = auditedObject?.CreatedAt ?? this.dateTimeOffsetProvider.UtcNow,
					CreatedBy = auditedObject?.CreatedBy,
					AfterCreatedState = JsonSerializer.Serialize(item)
				}
			};

			await this.InitializeAsync(message, item);

			// Publish the event message on the message bus.
			await this.publishEndpoint.Publish(message);
		}

		/// <summary>
		///     Initialize the event message from the underlying item.
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
