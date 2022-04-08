namespace Fluxera.Extensions.Hosting.Modules.DataManagement
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for contributors that make up the data seeding.
	/// </summary>
	[PublicAPI]
	public interface IDataSeedingContributor
	{
		/// <summary>
		///     Check if the contributor needs to be run.
		/// </summary>
		/// <returns></returns>
		Task<bool> NeedsDataSeedAsync();

		/// <summary>
		///     Executes the data seeding of this contributor.
		/// </summary>
		/// <returns></returns>
		Task SeedAsync();
	}
}
