namespace Catalog.Infrastructure.Contributors
{
	using System;
	using Catalog.Infrastructure.Contexts;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class RepositoryContextContributor : IRepositoryContextContributor
	{
		/// <inheritdoc />
		public Type ConfigureRepositoryContext()
		{
			return typeof(CatalogRepositoryContext);
		}
	}
}
