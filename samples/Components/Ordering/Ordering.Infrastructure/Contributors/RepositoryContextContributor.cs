namespace Ordering.Infrastructure.Contributors
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;
	using Ordering.Infrastructure.Contexts;

	[UsedImplicitly]
	internal sealed class RepositoryContextContributor : IRepositoryContextContributor
	{
		/// <inheritdoc />
		public Type ConfigureRepositoryContext()
		{
			return typeof(OrderingRepositoryContext);
		}
	}
}
