namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a contributor that provides the context for the repository it is registered for.
	/// </summary>
	[PublicAPI]
	public interface IRepositoryContextContributor
	{
		/// <summary>
		///     Configure the context type to use with the repository.
		/// </summary>
		/// <returns></returns>
		Type ConfigureRepositoryContext();
	}
}
