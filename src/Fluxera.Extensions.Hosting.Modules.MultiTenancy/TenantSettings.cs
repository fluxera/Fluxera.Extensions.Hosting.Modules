namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     A class holding a tenant's settings.
	/// </summary>
	[PublicAPI]
	public sealed class TenantSettings
	{
		/// <summary>
		///     Creates a new instance of the <see cref="TenantSettings" /> type.
		/// </summary>
		public TenantSettings() : this(null, null)
		{
		}

		/// <summary>
		///     Creates a new instance of the <see cref="TenantSettings" /> type.
		/// </summary>
		/// <param name="connectionStrings"></param>
		/// <param name="properties"></param>
		public TenantSettings(ConnectionStrings connectionStrings, TenantProperties properties)
		{
			this.ConnectionStrings = connectionStrings ?? new ConnectionStrings();
			this.Properties = properties ?? new TenantProperties();
		}

		/// <summary>
		///     Gets the connection strings of the tenant.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; }

		/// <summary>
		///		Gets or sets the tenant's additional properties.
		/// </summary>
		public TenantProperties Properties { get; set; }
	}
}
