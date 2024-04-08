#define MONGO
#undef EFCORE

namespace Ordering.Infrastructure
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Ordering.Domain;
	using Ordering.Domain.Customers;
	using Ordering.Domain.Orders;
	using Ordering.Infrastructure.Contributors;
	using Ordering.Infrastructure.Customers;
	using Ordering.Infrastructure.Orders;

	/// <summary>
	///     The infrastructure module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<OrderingDomainModule>]
	[DependsOn<MessagingModule>]
#if EFCORE
	[DependsOn<EntityFrameworkCorePersistenceModule>]
#elif MONGO
	[DependsOn<MongoPersistenceModule>]
#endif
	[DependsOn<ConfigurationModule>]
	public sealed class OrderingInfrastructureModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository contributor for the 'Default' repository.
			context.Services.AddRepositoryContributor<RepositoryContributor>();

			// Add repositories.
			context.Log("AddRepositories", services =>
			{
				services.TryAddTransient<IOrderRepository, OrderRepository>();
				services.TryAddTransient<ICustomerRepository, CustomerRepository>();
			});
		}
	}
}
