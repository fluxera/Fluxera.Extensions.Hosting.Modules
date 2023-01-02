namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy.UnitTests
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
