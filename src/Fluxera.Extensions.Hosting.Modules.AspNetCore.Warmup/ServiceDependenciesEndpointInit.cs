namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	[UsedImplicitly]
	internal sealed class ServiceDependenciesEndpointInit : IEndpointInit
	{
		private readonly ILogger logger;
		private readonly IServiceProvider provider;
		private readonly IServiceCollection services;

		public ServiceDependenciesEndpointInit(
			ILoggerFactory loggerFactory,
			IServiceCollection services,
			IServiceProvider provider)
		{
			logger = loggerFactory.CreateLogger("Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup.EndpointInit");
			this.services = services;
			this.provider = provider;
		}

		/// <inheritdoc />
		public Task InitializeEndpointsAsync()
		{
			logger.LogInformation("Initializing endpoint services dependencies.");

			using(IServiceScope scope = provider.CreateScope())
			{
				IEnumerable<Type> enumerable = GetServices(services);
				foreach(Type service in enumerable)
				{
					scope.ServiceProvider.GetServices(service);
				}
			}

			return Task.CompletedTask;
		}

		private static IEnumerable<Type> GetServices(IServiceCollection services)
		{
			return services
				.Where(descriptor => descriptor.ServiceType != typeof(IEndpointInit))
				.Where(descriptor => descriptor.ServiceType.ContainsGenericParameters == false)
				.Select(descriptor => descriptor.ServiceType)
				.Distinct();
		}
	}
}
