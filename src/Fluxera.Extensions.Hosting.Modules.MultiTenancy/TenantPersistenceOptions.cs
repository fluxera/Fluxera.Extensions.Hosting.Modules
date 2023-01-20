namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	///     The tenant persistence options.
	/// </summary>
	[PublicAPI]
	public sealed class TenantPersistenceOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="TenantPersistenceOptions" /> type.
		/// </summary>
		public TenantPersistenceOptions()
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
