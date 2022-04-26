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
		///     Creates a new instance of the <see cref="MultiTenancyOptions" /> type.
		/// </summary>
		public MultiTenancyOptions()
		{
			this.Repositories = new TenantOptionsDictionary();
		}

		/// <summary>
		///     Gets or sets the tenant options dictionary.
		/// </summary>
		public TenantOptionsDictionary Repositories { get; set; }
	}
}
