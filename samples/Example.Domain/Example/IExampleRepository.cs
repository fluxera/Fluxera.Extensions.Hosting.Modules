namespace Catalog.Domain.Example
{
	using Catalog.Domain.Shared.Example;
	using Fluxera.Repository;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a repository that handles example instances.
	/// </summary>
	[PublicAPI]
	public interface IExampleRepository : IRepository<Example, ExampleId>
	{
	}
}
