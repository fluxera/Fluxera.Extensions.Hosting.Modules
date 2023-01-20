namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     A database connection string provider that gets the name from a tenant or the configuration.
	/// </summary>
	[PublicAPI]
	public class TenantDatabaseConnectionStringProvider : DefaultDatabaseConnectionStringProvider
	{
		private readonly ITenantContextProvider tenantContextProvider;
		private readonly MultiTenancyOptions multiTenancyOptions;
		private readonly MultiTenancyPersistenceOptions multiTenancyPersistenceOptions;

		/// <inheritdoc />
		public TenantDatabaseConnectionStringProvider(
			ITenantContextProvider tenantContextProvider,
			IOptions<MultiTenancyOptions> multiTenancyOptions,
			IOptions<MultiTenancyPersistenceOptions> multiTenancyPersistenceOptions,
			IOptions<PersistenceOptions> repositoryOptions) : base(repositoryOptions)
		{
			this.tenantContextProvider = tenantContextProvider;
			this.multiTenancyOptions = multiTenancyOptions.Value;
			this.multiTenancyPersistenceOptions = multiTenancyPersistenceOptions.Value;
		}

		/// <inheritdoc />
		public override string GetConnectionString(RepositoryName repositoryName)
		{
			TenantPersistenceOptions tenantPersistenceOptions = this.multiTenancyPersistenceOptions.Repositories[repositoryName.Name];

			string connectionString;

			if(tenantPersistenceOptions.Enabled && tenantPersistenceOptions.Mode == MultiTenancyMode.DatabasePerTenant)
			{
				// A tenant is used to create the database connection string.
				// All other data belonging to the tenant is stored in a separate database for each tenant.
				TenantContext tenantContext = this.tenantContextProvider.GetTenantContext();

				// Get the (optional) database connection string from the tenant context data.
				// Use the default database connection string, if no connection string was provided for the tenant.
				connectionString = !tenantContext.TenantSettings.ConnectionStrings.ContainsKey(this.multiTenancyOptions.DatabaseConnectionStringName)
					? base.GetConnectionString(repositoryName)
					: tenantContext.TenantSettings.ConnectionStrings.GetOrDefault(this.multiTenancyOptions.DatabaseConnectionStringName);
			}
			else
			{
				// No tenant is needed to created the database connection string.
				// Use the default database connection string, if the aggregate belongs to it.
				connectionString = base.GetConnectionString(repositoryName);
			}

			return connectionString;
		}
	}
}
