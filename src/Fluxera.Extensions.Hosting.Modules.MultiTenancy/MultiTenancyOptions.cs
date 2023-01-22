namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the multi-tenancy module.
	/// </summary>
	[PublicAPI]
	public sealed class MultiTenancyOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="MultiTenancyPersistenceOptions" /> type.
		/// </summary>
		public MultiTenancyOptions()
		{
			this.Tenants = new TenantOptionsDictionary();
		}

		/// <summary>
		///     Gets or sets the tenant options dictionary.
		/// </summary>
		public TenantOptionsDictionary Tenants { get; set; }

		/// <summary>
		///		Gets or set the name od the connection string to use for the special tenant's database.
		/// </summary>
		public string DatabaseConnectionStringName { get; set; } = "Database";
	}
}
