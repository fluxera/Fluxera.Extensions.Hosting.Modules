namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	/// <summary>
	///		A database name provider that gets the name from a tenant or the configuration.
	/// </summary>
	[PublicAPI]
	public class TenantDatabaseNameProvider : DefaultDatabaseNameProvider
	{
		private readonly MultiTenancyPersistenceOptions multiTenancyPersistenceOptions;
		private readonly ITenantContextProvider tenantContextProvider;

		/// <inheritdoc />
		public TenantDatabaseNameProvider(
			ITenantContextProvider tenantContextProvider,
			IOptions<MultiTenancyPersistenceOptions> multiTenancyOptions,
			IOptions<PersistenceOptions> repositoryOptions) : base(repositoryOptions)
		{
			this.tenantContextProvider = tenantContextProvider;
			this.multiTenancyPersistenceOptions = multiTenancyOptions.Value;
		}

		/// <inheritdoc />
		public override string GetDatabaseName(RepositoryName repositoryName)
		{
			RepositoryOptions repositoryOptions = this.Options.Repositories[repositoryName.Name];
			TenantPersistenceOptions tenantPersistenceOptions = this.multiTenancyPersistenceOptions.Repositories[repositoryName.Name];

			string databaseName;

			if(tenantPersistenceOptions.Enabled && tenantPersistenceOptions.Mode == MultiTenancyMode.DatabasePerTenant)
			{
				// A tenant is used to create the database name.
				// All other data belonging to the tenant is stored in a separate database for each tenant.
				TenantContext tenantContext = this.tenantContextProvider.GetTenantContext();
				string tenantID = tenantContext.TenantID;

				databaseName = repositoryOptions.DatabaseNamePrefix.IsNullOrWhiteSpace()
					? $"{repositoryOptions.DatabaseName}-{tenantID}"
					: $"{repositoryOptions.DatabaseNamePrefix}-{repositoryOptions.DatabaseName}-{tenantID}";
			}
			else
			{
				// No tenant is needed to created the database name.
				// Use the default database name, if the aggregate belongs to it.
				databaseName = base.GetDatabaseName(repositoryName);
			}

			return databaseName;
		}
	}
}
