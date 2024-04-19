namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using JetBrains.Annotations;

	/// <summary>
	///     This interface is defined to standardize to set "Total Count of Items" to a dto.
	/// </summary>
	[PublicAPI]
	public interface IHasTotalCount
	{
		/// <summary>
		///     Total count of Items.
		/// </summary>
		long TotalCount { get; set; }
	}
}
