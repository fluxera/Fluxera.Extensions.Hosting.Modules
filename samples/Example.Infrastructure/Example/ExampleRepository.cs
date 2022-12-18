namespace Catalog.Infrastructure.Example
{
	using Catalog.Domain.Example;
	using Catalog.Domain.Shared.Example;
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
