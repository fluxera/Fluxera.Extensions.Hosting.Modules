namespace Ordering.Domain
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Ordering.Domain.CustomerAggregate;
	using Ordering.Domain.OrderAggregate;

	/// <summary>
	///     The domain module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<DomainModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class OrderingDomainModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add repositories.
			context.Log("AddRepositories", services =>
			{
				services.TryAddTransient<IOrderRepository, OrderRepository>();
				services.TryAddTransient<ICustomerRepository, CustomerRepository>();
			});
		}
	}
}
