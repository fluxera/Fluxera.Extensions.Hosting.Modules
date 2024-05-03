namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System;
	using MassTransit;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class SendEndpointConfigurator : ISendEndpointConfigurator
	{
		private readonly IServiceProvider serviceProvider;

		public SendEndpointConfigurator(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public ISendEndpointConfigurator MapSendEndpoint<T, TConsumer>()
			where T : class
			where TConsumer : class, IConsumer<T>
		{
			IEndpointNameFormatter endpointNameFormatter = this.serviceProvider.GetRequiredService<IEndpointNameFormatter>();

			string queueName = endpointNameFormatter.Consumer<TConsumer>();
			EndpointConvention.Map<T>(new Uri($"queue:{queueName}"));

			return this;
		}
	}
}
