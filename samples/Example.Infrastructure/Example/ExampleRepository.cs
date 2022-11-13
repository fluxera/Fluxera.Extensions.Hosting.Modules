namespace Example.Infrastructure.Example
{
	using Fluxera.Repository;
	using global::Example.Domain.Example;
	using global::Example.Domain.Shared.Example;
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
