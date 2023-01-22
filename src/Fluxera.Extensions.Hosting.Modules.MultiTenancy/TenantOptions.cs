namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     The tenant options.
	/// </summary>
	[PublicAPI]
	public sealed class TenantOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="TenantOptions" /> type.
		/// </summary>
		public TenantOptions()
		{
			this.ConnectionStrings = new ConnectionStrings();
			this.Properties = new TenantProperties();
		}

		/// <summary>
		///		Gets or sets the tenant's connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; set; }

		/// <summary>
		///		Gets or sets the tenant's additional properties.
		/// </summary>
		public TenantProperties Properties { get; set; }
	}
}
