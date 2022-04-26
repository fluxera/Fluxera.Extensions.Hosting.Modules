namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
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
		public TenantContext(string tenantID, string tenantName)
		{
			this.TenantID = tenantID;
			this.TenantName = tenantName;
		}

		/// <summary>
		///     Gets the ID of the tenant.
		/// </summary>
		public string TenantID { get; }

		/// <summary>
		///     Gets the name of the tenant.
		/// </summary>
		public string TenantName { get; }
	}
}
