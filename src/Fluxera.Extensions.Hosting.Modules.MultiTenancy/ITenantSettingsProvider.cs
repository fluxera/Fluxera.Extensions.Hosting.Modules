namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for a service that provides a tenant's settings.
	/// </summary>
	[PublicAPI]
	public interface ITenantSettingsProvider
	{
		/// <summary>
		///		Gets the tenant's settings.
		/// </summary>
		/// <param name="tenantID"></param>
		/// <returns></returns>
		TenantSettings GetTenantSettings(string tenantID);
	}
}
