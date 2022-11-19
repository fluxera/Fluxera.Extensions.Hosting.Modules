namespace Example.Domain.Example.Events
{
	using Fluxera.Repository.DomainEvents;
	using global::Example.Domain.Shared.Example;
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
