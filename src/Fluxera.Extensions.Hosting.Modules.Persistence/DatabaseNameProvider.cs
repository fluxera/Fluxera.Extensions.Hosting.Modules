//namespace Fluxera.Extensions.Hosting.Modules.Persistence
//{
//	using System;
//	using Fluxera.Repository;
//	using JetBrains.Annotations;

//	/// <summary>
//	///     A database name provider that utilizes the <see cref="IDatabaseNameProviderAdapter" /> to get the database name.
//	/// </summary>
//	/// <seealso cref="IDatabaseNameProvider" />
//	/// <seealso cref="IDatabaseNameProviderAdapter" />
//	[PublicAPI]
//	internal sealed class DatabaseNameProvider : IDatabaseNameProvider
//	{
//		private readonly IDatabaseNameProviderAdapter databaseNameProviderAdapter;
//		private readonly IRepositoryRegistry repositoryRegistry;

//		public DatabaseNameProvider(IDatabaseNameProviderAdapter databaseNameProviderAdapter, IRepositoryRegistry repositoryRegistry)
//		{
//			this.databaseNameProviderAdapter = databaseNameProviderAdapter;
//			this.repositoryRegistry = repositoryRegistry;
//		}

//		public string GetDatabaseName(Type aggregateRootType)
//		{
//			RepositoryName repositoryName = this.repositoryRegistry.GetRepositoryNameFor(aggregateRootType);
//			return this.databaseNameProviderAdapter.GetDatabaseName(repositoryName);
//		}
//	}
//}


