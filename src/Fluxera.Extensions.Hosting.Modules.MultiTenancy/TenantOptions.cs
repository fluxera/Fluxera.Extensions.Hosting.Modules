namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	///     The tenant options.
	/// </summary>
	[PublicAPI]
	public sealed class TenantOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="MultiTenancyOptions" /> type.
		/// </summary>
		public TenantOptions()
		{
			this.Enabled = false;
			this.Mode = MultiTenancyMode.DatabasePerTenant;
		}

		/// <summary>
		///     Flag, indicating if the multi-tenancy is enabled for the corresponding repository.
		/// </summary>
		[ConfigurationKeyName("MultiTenancy:Enabled")]
		public bool Enabled { get; set; }

		/// <summary>
		///     Gets or sets the multi-tenancy mode for the corresponding repository.
		/// </summary>
		[ConfigurationKeyName("MultiTenancy:Mode")]
		public MultiTenancyMode Mode { get; set; }
	}
}
