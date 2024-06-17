namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries
{
	using JetBrains.Annotations;

	/// <summary>
	///		A special application query contract for use with the Queries library.
	/// </summary>
	[PublicAPI]
	public interface ICountQuery : IApplicationQuery<long>
	{
	}
}