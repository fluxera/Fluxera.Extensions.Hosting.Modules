namespace Example.Domain.Example.Events
{
	using Fluxera.Repository.DomainEvents;
	using global::Example.Domain.Shared.Example;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class
		ExampleRemoved : ItemRemoved<Example, ExampleId>
	{
		/// <inheritdoc />
		public ExampleRemoved(ExampleId id, Example item)
			: base(id, item)
		{
		}
	}
}
