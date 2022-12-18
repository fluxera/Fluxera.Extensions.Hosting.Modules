namespace Catalog.Infrastructure.Example.Handlers
{
	using Catalog.Domain.Example;
	using Catalog.Domain.Shared.Example;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using ExampleAddedDomainEvent = global::Catalog.Domain.Example.Events.ExampleAdded;
	using ExampleAddedIntegrationEvent = global::Catalog.Domain.Shared.Example.Events.ExampleAdded;

	/// <summary>
	///     An event handler for bridging the <see cref="ExampleAddedDomainEvent" /> domain event
	///     to the <see cref="ExampleAddedIntegrationEvent" /> integration event message.
	/// </summary>
	[UsedImplicitly]
	public sealed class ExampleAddedHandler : ItemAddedEventHandlerBase<Example, ExampleId, ExampleAddedDomainEvent, ExampleAddedIntegrationEvent>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ExampleAddedHandler" /> class.
		/// </summary>
		/// <param name="publishEndpoint">The publish endpoint.</param>
		/// <param name="dateTimeOffsetProvider">The date time offset provider.</param>
		public ExampleAddedHandler(
			IPublishEndpoint publishEndpoint,
			IDateTimeOffsetProvider dateTimeOffsetProvider) : base(publishEndpoint, dateTimeOffsetProvider)
		{
		}
	}
}
