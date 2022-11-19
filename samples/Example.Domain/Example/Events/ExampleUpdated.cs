namespace Example.Domain.Example.Events
{
	using Fluxera.Repository.DomainEvents;
	using global::Example.Domain.Shared.Example;
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
