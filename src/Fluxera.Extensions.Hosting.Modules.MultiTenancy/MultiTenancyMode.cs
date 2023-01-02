namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using JetBrains.Annotations;

	/// <summary>
	///     An enum providing the supported multi-tenancy modes.
	/// </summary>
	[PublicAPI]
	public enum MultiTenancyMode
	{
		/// <summary>
		///     No multi-tenancy mode used.
		/// </summary>
		None,

		/// <summary>
		///     Use a single database and a discriminator column for the tenant.
		/// </summary>
		SingleDatabase,

		/// <summary>
		///     Use a separate database for each tenant.
		/// </summary>
		DatabasePerTenant
	}
}
