namespace Example.Domain.ExampleAggregate.Repositories
{
	using Example.Domain.ExampleAggregate.Model;
	using Fluxera.Repository;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a repository that handles example instances.
	/// </summary>
	[PublicAPI]
	public interface IExampleRepository : IRepository<Example, string>
	{
	}
}
