namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	[UsedImplicitly]
	internal sealed class TenantDatabaseConnectionStringProvider : DefaultDatabaseConnectionStringProvider
	{
		private readonly MultiTenancyOptions multiTenancyOptions;
		private readonly ITenantContextProvider tenantContextProvider;

		/// <inheritdoc />
		public TenantDatabaseConnectionStringProvider(
			ITenantContextProvider tenantContextProvider,
			IOptions<MultiTenancyOptions> multiTenancyOptions,
			IOptions<PersistenceOptions> repositoryOptions) : base(repositoryOptions)
		{
			this.tenantContextProvider = tenantContextProvider;
			this.multiTenancyOptions = multiTenancyOptions.Value;
		}

		/// <inheritdoc />
		public override string GetConnectionString(RepositoryName repositoryName)
		{
			TenantOptions tenantOptions = this.multiTenancyOptions.Repositories[repositoryName.Name];

			string connectionString;

			if(tenantOptions.Enabled && tenantOptions.Mode == MultiTenancyMode.DatabasePerTenant)
			{
				// A tenant is used to create the database connection string.
				// All other data belonging to the tenant is stored in a separate database for each tenant.
				TenantContext tenantContext = this.tenantContextProvider.GetTenantContext();

				// Get the (optional) database connection string from the tenant context data.
				// Use the default database connection string, if no connection string was provided for the tenant.
				connectionString = string.IsNullOrWhiteSpace(tenantContext.TenantConnectionString)
					? base.GetConnectionString(repositoryName)
					: tenantContext.TenantConnectionString;
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
