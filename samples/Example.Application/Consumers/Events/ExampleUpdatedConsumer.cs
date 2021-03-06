namespace Example.Application.Consumers.Events
{
	using System.Threading.Tasks;
	using Example.Domain.Shared.ExampleAggregate.Messages.Events;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A consumer implementation that consumes <see cref="ExampleUpdated" /> event messages.
	/// </summary>
	[UsedImplicitly]
	public sealed class ExampleUpdatedConsumer : IConsumer<ExampleUpdated>
	{
		/// <inheritdoc />
		public Task Consume(ConsumeContext<ExampleUpdated> context)
		{
			return Task.CompletedTask;
		}
	}
}
