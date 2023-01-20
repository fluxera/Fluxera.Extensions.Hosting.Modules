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
		public TenantSettings()
		{
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///     Gets the connection strings of the tenant.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; }
	}
}
