namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for entities that belong to a specific tenant.
	/// </summary>
	[PublicAPI]
	public interface IMultiTenancyObject
	{
		/// <summary>
		///     The ID of the tenant.
		/// </summary>
		string TenantID { get; set; }
	}
}
