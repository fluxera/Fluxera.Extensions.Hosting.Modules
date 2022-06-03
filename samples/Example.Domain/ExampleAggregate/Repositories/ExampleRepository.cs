namespace Example.Domain.ExampleAggregate.Repositories
{
	using Example.Domain.ExampleAggregate.Model;
	using Example.Domain.Shared.ExampleAggregate.Model;
	using Fluxera.Repository;
	using JetBrains.Annotations;

	/// <summary>
	///     An implementation of a generic repository that handles example instances.
	/// </summary>
	[UsedImplicitly]
	internal sealed class ExampleRepository : Repository<Example, ExampleId>, IExampleRepository
	{
		/// <inheritdoc />
		public ExampleRepository(IRepository<Example, ExampleId> innerRepository)
			: base(innerRepository)
		{
		}
	}
}
