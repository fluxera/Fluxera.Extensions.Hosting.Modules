namespace Fluxera.Extensions.Hosting.Modules.Persistence.UnitTests
{
	using Fluxera.Repository.InMemory;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class TestRepositoryContext : InMemoryContext
	{
		/// <inheritdoc />
		protected override void ConfigureOptions(InMemoryContextOptions options)
		{
		}
	}
}
