namespace Catalog.Domain.Example.Events
{
	using Catalog.Domain.Shared.Example;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ExampleRemoved : ItemRemoved<Example, ExampleId>
	{
		/// <inheritdoc />
		public ExampleRemoved(ExampleId id, Example item)
			: base(id, item)
		{
		}
	}
}
