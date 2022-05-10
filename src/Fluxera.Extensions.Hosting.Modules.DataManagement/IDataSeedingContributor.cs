namespace Fluxera.Extensions.Hosting.Modules.DataManagement
{
	using System;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for contributors that make up the data seeding.
	/// </summary>
	[PublicAPI]
	public interface IDataSeedingContributor
	{
		/// <summary>
		///     Executes the data seeding of this contributor.
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		Task ExecuteAsync(IServiceProvider serviceProvider);
	}
}
