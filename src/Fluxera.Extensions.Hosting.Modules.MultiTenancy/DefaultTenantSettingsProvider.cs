namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	/// <summary>
	///		A default tenant settings provider that reads the tenant's settings fron the options.
	/// </summary>
	[PublicAPI]
	public class DefaultTenantSettingsProvider : ITenantSettingsProvider
	{
		private readonly MultiTenancyOptions options;

		/// <summary>
		///		Initializes an new instance of the <see cref="DefaultTenantSettingsProvider"/> type.
		/// </summary>
		/// <param name="options"></param>
		public DefaultTenantSettingsProvider(IOptions<MultiTenancyOptions> options)
		{
			this.options = options.Value;
		}

		/// <inheritdoc />
		public TenantSettings GetTenantSettings(string tenantID)
		{
			TenantSettings tenantSettings = null;

			TenantOptions tenantOptions = this.options.Tenants.GetOrDefault(tenantID);
			if(tenantOptions is not null)
			{
				tenantSettings = new TenantSettings(tenantOptions.ConnectionStrings, tenantOptions.Properties);
			}

			return tenantSettings;
		}
	}
}
