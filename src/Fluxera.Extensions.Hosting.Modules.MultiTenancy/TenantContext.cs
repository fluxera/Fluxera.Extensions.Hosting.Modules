namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     A context holding information about the a tenant.
	/// </summary>
	[PublicAPI]
	public sealed class TenantContext
	{
		/// <summary>
		///     Creates a new instance of the <see cref="TenantContext" /> type.
		/// </summary>
		/// <param name="tenantID"></param>
		/// <param name="tenantName"></param>
		/// <param name="tenantDisplayName"></param>
		/// <param name="tenantSettings"></param>
		public TenantContext(string tenantID, string tenantName, string tenantDisplayName, TenantSettings tenantSettings)
		{
			this.TenantID = Guard.Against.NullOrWhiteSpace(tenantID);
			this.TenantName = tenantName;
			this.TenantDisplayName = tenantDisplayName;
			this.TenantSettings = tenantSettings ?? new TenantSettings();
		}

		/// <summary>
		///     Gets the ID of the tenant.
		/// </summary>
		public string TenantID { get; }

		/// <summary>
		///     Gets the name of the tenant.
		/// </summary>
		public string TenantName { get; }

		/// <summary>
		///     Gets the display name of the tenant.
		/// </summary>
		public string TenantDisplayName { get; }

		/// <summary>
		///		Gets the tenant's settings.
		/// </summary>
		public TenantSettings TenantSettings { get; }
	}
}
