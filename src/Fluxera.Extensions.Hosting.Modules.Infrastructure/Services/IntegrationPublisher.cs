namespace Fluxera.Extensions.Hosting.Modules.Infrastructure.Services
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class IntegrationPublisher : IIntegrationPublisher
	{
		private readonly IPublishEndpoint publishEndpoint;

		public IntegrationPublisher(IPublishEndpoint publishEndpoint)
		{
			this.publishEndpoint = publishEndpoint;
		}

		/// <inheritdoc />
		public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) 
			where TEvent : class, IIntegrationEvent
		{
			return this.publishEndpoint.Publish(@event, cancellationToken);
		}
	}
}
