namespace Catalog.Infrastructure.Example.Handlers
{
	using Catalog.Domain.Example;
	using Catalog.Domain.Shared.Example;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;
	using ExampleUpdatedDomainEvent = global::Catalog.Domain.Example.Events.ExampleUpdated;
	using ExampleUpdatedIntegrationEvent = global::Catalog.Domain.Shared.Example.Events.ExampleUpdated;

	/// <summary>
	///     An event handler for bridging the <see cref="ExampleUpdatedDomainEvent" /> domain event
	///     to the <see cref="ExampleUpdatedIntegrationEvent" /> integration event message.
	/// </summary>
	[UsedImplicitly]
	public sealed class ExampleUpdatedHandler : ItemUpdatedEventHandlerBase<Example, ExampleId, ExampleUpdatedDomainEvent, ExampleUpdatedIntegrationEvent>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ExampleUpdatedHandler" /> class.
		/// </summary>
		/// <param name="publishEndpoint">The publish endpoint.</param>
		/// <param name="dateTimeOffsetProvider">The date time offset provider.</param>
		public ExampleUpdatedHandler(
			IPublishEndpoint publishEndpoint,
			IDateTimeOffsetProvider dateTimeOffsetProvider) : base(publishEndpoint, dateTimeOffsetProvider)
		{
		}
	}
}
