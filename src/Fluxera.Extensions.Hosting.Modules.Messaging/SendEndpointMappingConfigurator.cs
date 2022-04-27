namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System;
	using MassTransit;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class SendEndpointMappingConfigurator : ISendEndpointMappingConfigurator
	{
		private readonly IServiceProvider serviceProvider;

		public SendEndpointMappingConfigurator(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public ISendEndpointMappingConfigurator MapSendEndpoint<T, TConsumer>()
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
