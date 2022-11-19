namespace Example.Infrastructure.Example.Handlers
{
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using global::Example.Domain.Example;
	using global::Example.Domain.Shared.Example;
	using JetBrains.Annotations;
	using MassTransit;
	using ExampleRemovedDomainEvent = global::Example.Domain.Example.Events.ExampleRemoved;
	using ExampleRemovedIntegrationEvent = global::Example.Domain.Shared.Example.Events.ExampleRemoved;

	/// <summary>
	///     An event handler for bridging the <see cref="ExampleRemovedDomainEvent" /> domain event
	///     to the <see cref="ExampleRemovedIntegrationEvent" /> integration event message.
	/// </summary>
	[UsedImplicitly]
	public sealed class ExampleRemovedHandler : ItemRemovedEventHandlerBase<Example, ExampleId, ExampleRemovedDomainEvent, ExampleRemovedIntegrationEvent>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ExampleRemovedHandler" /> class.
		/// </summary>
		/// <param name="publishEndpoint">The publish endpoint.</param>
		/// <param name="dateTimeOffsetProvider">The date time offset provider.</param>
		public ExampleRemovedHandler(
			IPublishEndpoint publishEndpoint,
			IDateTimeOffsetProvider dateTimeOffsetProvider) : base(publishEndpoint, dateTimeOffsetProvider)
		{
		}
	}
}
