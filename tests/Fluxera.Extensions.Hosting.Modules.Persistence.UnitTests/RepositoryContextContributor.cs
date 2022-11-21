namespace Fluxera.Extensions.Hosting.Modules.Persistence.UnitTests
{
	using System;

	public class RepositoryContextContributor : IRepositoryContextContributor
	{
		/// <inheritdoc />
		public Type ConfigureRepositoryContext()
		{
			return typeof(TestContext);
		}
	}
}
