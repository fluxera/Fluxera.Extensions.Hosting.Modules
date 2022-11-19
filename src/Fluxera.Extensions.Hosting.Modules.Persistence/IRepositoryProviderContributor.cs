namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using Fluxera.Repository;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a repository provider contributor.
	/// </summary>
	[PublicAPI]
	public interface IRepositoryProviderContributor
	{
		/// <summary>
		///     The name of the repository provider.
		/// </summary>
		public string RepositoryProviderName { get; }

		/// <summary>
		///     Gets an action that adds a repository provided by this contributor.
		/// </summary>
		Action<IRepositoryBuilder, string, Type, Action<IRepositoryOptionsBuilder>, IServiceConfigurationContext> AddRepository { get; }
	}
}
