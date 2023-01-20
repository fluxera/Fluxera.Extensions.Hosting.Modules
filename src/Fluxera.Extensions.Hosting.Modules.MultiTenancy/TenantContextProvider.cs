namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using System;
	using System.Security.Claims;
	using Fluxera.Extensions.Hosting.Modules.Principal;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class TenantContextProvider : ITenantContextProvider
	{
		private readonly IPrincipalAccessor principalAccessor;
		private readonly ITenantSettingsProvider tenantSettingsProvider;

		public TenantContextProvider(
			IPrincipalAccessor principalAccessor,
			ITenantSettingsProvider tenantSettingsProvider = null)
		{
			this.principalAccessor = principalAccessor;
			this.tenantSettingsProvider = tenantSettingsProvider;
		}

		/// <inheritdoc />
		public TenantContext GetTenantContext()
		{
			bool tenantContextAvailable = this.TryGetTenantContext(out TenantContext tenantContext);
			if(!tenantContextAvailable)
			{
				throw new InvalidOperationException("No tenant available in the principal claims.");
			}

			return tenantContext;
		}

		public bool TryGetTenantContext(out TenantContext tenantContext)
		{
			ClaimsPrincipal claimsPrincipal = this.principalAccessor.User;
			string tenantID = claimsPrincipal?.GetClaimValue(TenantClaimTypes.TenantID);
			string tenantName = claimsPrincipal?.GetClaimValue(TenantClaimTypes.TenantName);
			string tenantDisplayName = claimsPrincipal?.GetClaimValue(TenantClaimTypes.TenantDisplayName);

			if(string.IsNullOrWhiteSpace(tenantID))
			{
				tenantContext = null;
				return false;
			}

			TenantSettings tenantSettings = this.tenantSettingsProvider?.GetTenantSettings(tenantID);
			
			//ConnectionStrings tenantConnectionStrings = null;
			//IList<string> connectionStringKeyValuePairs = claimsPrincipal?.GetClaimValues(TenantClaimTypes.TenantConnectionString).ToList();
			//if(connectionStringKeyValuePairs != null && connectionStringKeyValuePairs.Any())
			//{
			//	tenantConnectionStrings = new ConnectionStrings();

			//	foreach(string connectionStringKeyValuePair in connectionStringKeyValuePairs)
			//	{
			//		string key = connectionStringKeyValuePair.Split('|').FirstOrDefault("Default");
			//		string connectionString = connectionStringKeyValuePair.Split('|').LastOrDefault() ?? connectionStringKeyValuePair;

			//		tenantConnectionStrings.Add(key, connectionString);
			//	}
			//}

			tenantContext = new TenantContext(tenantID, tenantName, tenantDisplayName, tenantSettings);
			return true;
		}
	}
}
