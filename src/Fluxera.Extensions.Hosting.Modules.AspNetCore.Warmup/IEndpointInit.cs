namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a warmup routine.
	/// </summary>
	[PublicAPI]
	public interface IEndpointInit
	{
		/// <summary>
		///     Executed the warmup routine.
		/// </summary>
		/// <returns></returns>
		Task InitializeAsync();
	}
}
