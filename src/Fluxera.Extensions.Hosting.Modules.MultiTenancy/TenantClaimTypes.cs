namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using JetBrains.Annotations;

	/// <summary>
	///     Contains custom tenant-related claim types.
	/// </summary>
	[PublicAPI]
	public static class TenantClaimTypes
	{
		/// <summary>
		///     The claim type for the tenant ID.
		/// </summary>
		/// <remarks>
		///     Default: "tenant-id".
		/// </remarks>
		public const string TenantID = "tenant-id";

		/// <summary>
		///     The claim type for the tenant name.
		/// </summary>
		/// <remarks>
		///     Default: "tenant-name".
		/// </remarks>
		public const string TenantName = "tenant-name";

		/// <summary>
		///     The claim type for the tenant display name.
		/// </summary>
		/// <remarks>
		///     Default: "tenant-display-name".
		/// </remarks>
		public const string TenantDisplayName = "tenant-display-name";
	}
}
