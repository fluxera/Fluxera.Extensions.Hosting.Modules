namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Repository;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	[UsedImplicitly]
	internal sealed class TenantDatabaseNameProvider : DefaultDatabaseNameProvider
	{
		private readonly MultiTenancyOptions multiTenancyOptions;
		private readonly ITenantContextProvider tenantContextProvider;

		/// <inheritdoc />
		public TenantDatabaseNameProvider(
			ITenantContextProvider tenantContextProvider,
			IOptions<MultiTenancyOptions> multiTenancyOptions,
			IOptions<PersistenceOptions> repositoryOptions) : base(repositoryOptions)
		{
			this.tenantContextProvider = tenantContextProvider;
			this.multiTenancyOptions = multiTenancyOptions.Value;
		}

		/// <inheritdoc />
		public override string GetDatabaseName(RepositoryName repositoryName)
		{
			RepositoryOptions repositoryOptions = this.Options.Repositories[repositoryName.Name];
			TenantOptions tenantOptions = this.multiTenancyOptions.Repositories[repositoryName.Name];

			string databaseName;

			if(tenantOptions.Enabled && tenantOptions.Mode == MultiTenancyMode.DatabasePerTenant)
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
