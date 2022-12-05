namespace Example.Infrastructure
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.InMemory;
	using Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore;
	using global::Example.Domain;
	using global::Example.Domain.Example;
	using global::Example.Infrastructure.Contributors;
	using global::Example.Infrastructure.Example;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     The infrastructure module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<ExampleDomainModule>]
	[DependsOn<InMemoryMessagingModule>]
	[DependsOn<TransactionalOutboxModule<ExampleDbContext>>]
	[DependsOn<EntityFrameworkCorePersistenceModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class ExampleInfrastructureModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository contributor for the 'Default' repository.
			context.Services.AddRepositoryContributor<RepositoryContributor>("Default");

			// Add the repository context contributor for the 'Default' repository.
			context.Services.AddRepositoryContextContributor<RepositoryContextContributor>("Default");

			// Add repositories.
			context.Log("AddRepositories", services =>
				services.TryAddTransient<IExampleRepository, ExampleRepository>());

			context.Services.AddDbContext<ExampleDbContext>();
		}
	}
}
