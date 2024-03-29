﻿namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using Fluxera.Repository;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a database name provider.
	/// </summary>
	[PublicAPI]
	public interface IDatabaseNameProvider
	{
		/// <summary>
		///     Gets the database name for the given repository name.
		/// </summary>
		/// <param name="repositoryName"></param>
		/// <returns></returns>
		string GetDatabaseName(RepositoryName repositoryName);
	}
}
