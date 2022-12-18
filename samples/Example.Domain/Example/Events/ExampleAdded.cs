namespace Catalog.Domain.Example.Events
{
	using Catalog.Domain.Shared.Example;
	using Fluxera.Repository.DomainEvents;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ExampleAdded : ItemAdded<Example, ExampleId>
	{
		/// <inheritdoc />
		public ExampleAdded(Example item)
			: base(item)
		{
		}
	}
}
