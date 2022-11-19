namespace Example.Application.Consumers
{
	using System.Threading.Tasks;
	using Example.Domain.Shared.Example.Events;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A consumer implementation that consumes <see cref="ExampleAdded" /> event messages.
	/// </summary>
	[UsedImplicitly]
	public sealed class ExampleAddedConsumer : IConsumer<ExampleAdded>
	{
		/// <inheritdoc />
		public Task Consume(ConsumeContext<ExampleAdded> context)
		{
			return Task.CompletedTask;
		}
	}
}
