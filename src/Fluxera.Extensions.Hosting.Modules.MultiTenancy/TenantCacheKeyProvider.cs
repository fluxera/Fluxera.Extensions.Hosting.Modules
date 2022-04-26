namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using System;
	using Fluxera.Repository;
	using Fluxera.Repository.Caching;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	[UsedImplicitly]
	internal sealed class TenantCacheKeyProvider : DefaultCacheKeyProvider
	{
		private readonly MultiTenancyOptions options;
		private readonly ITenantContextProvider tenantContextProvider;

		public TenantCacheKeyProvider(
			ITenantContextProvider tenantContextProvider,
			IOptions<MultiTenancyOptions> options)
		{
			this.tenantContextProvider = tenantContextProvider;
			this.options = options.Value;
		}

		/// <summary>
		///     Creates a key prefix like this: Repositories/Books/Acme.Books.Domain.Model.Book/{Tenant}
		/// </summary>
		/// <param name="repositoryName"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		protected override string GetCachePrefix(RepositoryName repositoryName, Type type)
		{
			string cachePrefix = base.GetCachePrefix(repositoryName, type);

			TenantOptions tenantOptions = this.options.Repositories[repositoryName.Value];

			if(tenantOptions.Enabled)
			{
				// Repositories/Books/Acme.Books.Domain.Model.Book/{Tenant}
				TenantContext tenantContext = this.tenantContextProvider.GetTenantContext();
				string tenantID = tenantContext.TenantID;
				cachePrefix = $"{cachePrefix}/{tenantID}";
			}

			return cachePrefix;
		}
	}
}
