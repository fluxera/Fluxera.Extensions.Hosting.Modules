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
		///     Default: "tenant-id".
		/// </summary>
		public static string TenantID { get; set; } = "tenant-id";

		/// <summary>
		///     Default: "tenant-name".
		/// </summary>
		public static string TenantName { get; set; } = "tenant-name";
	}
}
