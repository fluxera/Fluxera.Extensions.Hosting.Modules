namespace Fluxera.Extensions.Hosting.Modules.Persistence.UnitTests
{
	using Fluxera.Repository.InMemory;

	public class TestContext : InMemoryContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(InMemoryContextOptions options)
		{
			options.Database = "test";
		}
	}
}
