namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB.Sequence
{
	using Fluxera.Repository;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a factory that creates <see cref="ISequenceService" /> instances.
	/// </summary>
	[PublicAPI]
	public interface ISequenceServiceFactory
	{
		/// <summary>
		///     Creates a sequence service for the given repository name.
		/// </summary>
		/// <param name="repositoryName"></param>
		/// <returns></returns>
		ISequenceService CreateSequenceService(RepositoryName repositoryName);
	}
}
