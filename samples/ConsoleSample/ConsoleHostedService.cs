namespace ConsoleSample
{
	using ConsoleSample.Contracts;
	using MassTransit;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;

	public sealed class ConsoleHostedService : BackgroundService
	{
		private readonly ILogger<ConsoleHostedService> logger;
		private readonly IPublishEndpoint publishEndpoint;

		public ConsoleHostedService(
			ILogger<ConsoleHostedService> logger,
			IPublishEndpoint publishEndpoint)
		{
			this.logger = logger;
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while(!stoppingToken.IsCancellationRequested)
			{
				this.logger.LogInformation("Publishing");

				PackageSent message = new PackageSent();
				await this.publishEndpoint.Publish(message, stoppingToken);

				await Task.Delay(1000, stoppingToken);
			}
		}
	}
}
