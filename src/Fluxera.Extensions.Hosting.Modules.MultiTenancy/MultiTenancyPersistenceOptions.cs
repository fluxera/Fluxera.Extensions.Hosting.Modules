namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the persistence part of the multi-tenancy module.
	/// </summary>
	[PublicAPI]
	public sealed class MultiTenancyPersistenceOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="MultiTenancyPersistenceOptions" /> type.
		/// </summary>
		public MultiTenancyPersistenceOptions()
		{
			this.Repositories = new TenantPersistenceOptionsDictionary();
		}

		/// <summary>
		///     Gets or sets the tenant persistence options dictionary.
		/// </summary>
		public TenantPersistenceOptionsDictionary Repositories { get; set; }
	}
}
