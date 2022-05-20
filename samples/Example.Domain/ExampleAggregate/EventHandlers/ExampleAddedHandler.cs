namespace Example.Domain.ExampleAggregate.EventHandlers
{
	using Example.Domain.ExampleAggregate.Model;
	using Example.Domain.Shared.ExampleAggregate.Messages.Events;
	using Fluxera.Entity.DomainEvents;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     An event handler for bridging the <see cref="ItemAdded{TAggregateRoot,TKey}" /> domain event
	///     to the <see cref="ExampleAdded" /> messaging event message.
	/// </summary>
	[UsedImplicitly]
	public sealed class ExampleAddedHandler : ItemAddedEventHandlerBase<Example, string, ExampleAdded>
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
