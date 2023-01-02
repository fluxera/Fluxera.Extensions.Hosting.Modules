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
			this.Mode = MultiTenancyMode.None;
		}

		/// <summary>
		///     Flag, indicating if the multi-tenancy is enabled for the corresponding repository.
		/// </summary>
		public bool Enabled => this.Mode != MultiTenancyMode.None;

		/// <summary>
		///     Gets or sets the multi-tenancy mode for the corresponding repository.
		/// </summary>
		[ConfigurationKeyName("MultiTenancy:Mode")]
		public MultiTenancyMode Mode { get; set; }
	}
}
