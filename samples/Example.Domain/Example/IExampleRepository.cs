namespace Example.Domain.Example
{
	using Fluxera.Repository;
	using global::Example.Domain.Shared.Example;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a repository that handles example instances.
	/// </summary>
	[PublicAPI]
	public interface IExampleRepository : IRepository<Example, ExampleId>
	{
	}
}
