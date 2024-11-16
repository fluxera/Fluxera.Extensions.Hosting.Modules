namespace SampleApp.Infrastructure
{
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Infrastructure;
	using Fluxera.Extensions.Hosting.Modules.Infrastructure.Queries;
	using Fluxera.Extensions.Hosting.Modules.Messaging.InMemory;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB;
	using Fluxera.Queries;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using SampleApp.Application.Contracts.Customers;
	using SampleApp.Application.Contracts.Customers.FindCustomers;
	using SampleApp.Application.Contracts.Customers.GetCustomer;
	using SampleApp.Application.Contracts.Customers.GetCustomerCount;
	using SampleApp.Domain.Customers;
	using SampleApp.Infrastructure.Contributors;
	using SampleApp.Infrastructure.Customers;

	/// <summary>
	///		The infrastructure module.
	/// </summary>
	[PublicAPI]
	[DependsOn<MongoPersistenceModule>]
	[DependsOn<InMemoryMessagingModule>]
	[DependsOn<DataManagementModule>]
	[DependsOn<InfrastructureModule>]
	public sealed class SampleAppInfrastructureModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository contributor for the 'Default' repository.
			context.Services.AddRepositoryContributor<RepositoryContributor>();

			// Add the data seeding contributor.
			context.Services.AddDataSeedingContributor<DataSeedingContributor>();

			// Add repositories.
			context.Log("AddRepositories", services =>
			{
				services.AddTransient<ICustomerRepository, CustomerRepository>();
			});

			// Add infrastructure services.
			context.Log("AddDateTimeOffsetProvider", 
				services => services.AddDateTimeOffsetProvider());

			// Add the data queries services and entity sets configuration.
			context.Log("AddDataQueries", services =>
			{
				services.AddDataQueries(options =>
				{
					options.EntitySet<CustomerDto>("Customers", "Customer", entityType =>
					{
						entityType
							.HasKey(x => x.ID)
							.Ignore(x => x.IgnoreMe);
					})
					.UseFind<FindCustomersQuery>()
					.UseGet<GetCustomerQuery>()
					.UseCount<GetCustomerCountQuery>();

					options.ComplexType<AddressDto>("Address", complexType =>
					{
						complexType.Ignore(x => x.IgnoreMe);
					});

					options.ComplexType<CountryDto>("Country");
				});
			});

			// Add delegating query executor services.
			context.Log("AddDelegatingQueryExecutor",
				services => services.AddDelegatingQueryExecutor());
		}
	}
}
