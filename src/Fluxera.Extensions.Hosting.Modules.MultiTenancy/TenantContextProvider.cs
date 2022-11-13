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

		public TenantContextProvider(IPrincipalAccessor principalAccessor)
		{
			this.principalAccessor = principalAccessor;
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
			string tenantConnectionString = claimsPrincipal?.GetClaimValue(TenantClaimTypes.TenantConnectionString);

			if(string.IsNullOrWhiteSpace(tenantID))
			{
				tenantContext = null;
				return false;
			}

			tenantContext = new TenantContext(tenantID, tenantName, tenantConnectionString);
			return true;
		}
	}
}
