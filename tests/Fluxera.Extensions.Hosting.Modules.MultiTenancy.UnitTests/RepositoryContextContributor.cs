namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy.UnitTests
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Persistence;

	public class RepositoryContextContributor : IRepositoryContextContributor
	{
		/// <inheritdoc />
		public Type ConfigureRepositoryContext()
		{
			return typeof(TestContext);
		}
	}
}
