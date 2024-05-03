namespace Fluxera.Extensions.Hosting.Modules.Infrastructure.Services
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MassTransit;

	[UsedImplicitly]
	internal sealed class IntegrationSender : IIntegrationSender
	{
		private readonly ISendEndpointProvider sendEndpointProvider;

		public IntegrationSender(ISendEndpointProvider sendEndpointProvider)
		{
			this.sendEndpointProvider = sendEndpointProvider;
		}

		/// <inheritdoc />
		public Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, IIntegrationCommand
		{
			return this.sendEndpointProvider.Send(command, cancellationToken: cancellationToken);
		}
	}
}
