namespace ConsoleSample.Consumers
{
	using ConsoleSample.Contracts;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Logging;

	[UsedImplicitly]
	internal sealed class PackageSentConsumer : IConsumer<PackageSent>
	{
		private readonly ILogger<PackageSentConsumer> logger;

		public PackageSentConsumer(ILogger<PackageSentConsumer> logger)
		{
			this.logger = logger;
		}

		/// <inheritdoc />
		public async Task Consume(ConsumeContext<PackageSent> context)
		{
			this.logger.LogInformation("Consumed");
		}
	}
}
