namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.MultiTenancy.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using Fluxera.Repository.Caching;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A module that enabled multi-tenancy.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(PersistenceModule))]
	[DependsOn(typeof(PrincipalModule))]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class MultiTenancyModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the tenant context provider service.
			context.Log("AddTenantContextProvider",
				services => services.TryAddTransient<ITenantContextProvider, TenantContextProvider>());

			// Add services for multiple, per-tenant databases.
			context.Log("AddTenantDatabaseNameProvider",
				services => services.ReplaceTransient<IDatabaseNameProvider, TenantDatabaseNameProvider>());

			// Add services for connection-string-based, multiple, per-tenant databases.
			context.Log("AddTenantDatabaseNameProvider",
				services => services.ReplaceTransient<IDatabaseConnectionStringProvider, TenantDatabaseConnectionStringProvider>());

			// Add services for single, multi-tenant database.
			// Note: This must be done using tenant interceptors.

			context.Log("AddTenantCacheKeyProvider",
				services => services.ReplaceTransient<ICacheKeyProvider, TenantCacheKeyProvider>());
		}
	}
}
