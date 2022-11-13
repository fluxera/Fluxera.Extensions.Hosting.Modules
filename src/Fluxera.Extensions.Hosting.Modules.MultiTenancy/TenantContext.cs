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
		/// <param name="tenantConnectionString"></param>
		public TenantContext(string tenantID, string tenantName, string tenantConnectionString)
		{
			this.TenantID = Guard.Against.NullOrWhiteSpace(tenantID);
			this.TenantName = tenantName;
			this.TenantConnectionString = tenantConnectionString;
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
		///     Gets the connection string of the tenant.
		/// </summary>
		public string TenantConnectionString { get; }
	}
}
