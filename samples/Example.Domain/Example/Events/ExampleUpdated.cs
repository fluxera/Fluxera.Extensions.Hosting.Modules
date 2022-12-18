namespace Catalog.Domain.Example.Events
{
	using Catalog.Domain.Shared.Example;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ExampleUpdated : ItemUpdated<Example, ExampleId>
	{
		/// <inheritdoc />
		public ExampleUpdated(Example beforeUpdateItem, Example afterUpdateItem)
			: base(beforeUpdateItem, afterUpdateItem)
		{
		}
	}
}
