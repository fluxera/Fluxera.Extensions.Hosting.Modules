namespace Example.Domain.Example
{
	using Fluxera.Entity;
	using global::Example.Domain.Example.Events;
	using global::Example.Domain.Shared.Example;
	using JetBrains.Annotations;

	/// <summary>
	///     An aggregate root holding the information of an example.
	/// </summary>
	/// <seealso cref="AggregateRoot{TAggregateRoot,TKey}" />
	[PublicAPI]
	public sealed class Example : AggregateRoot<Example, ExampleId>
	{
		/// <summary>
		///     Gets or sets the name of the example.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Gets or sets the kind of the example.
		/// </summary>
		public ExampleKind Kind { get; set; }

		/// <summary>
		///     Gets or sets the example detail.
		/// </summary>
		public ExampleDetail Detail { get; set; }

		public void MakeExampleMoreDifficult()
		{
			this.Kind = this.Kind switch
			{
				ExampleKind.Easy => ExampleKind.Medium,
				ExampleKind.Medium => ExampleKind.Hard,
				_ => this.Kind
			};

			this.RaiseDomainEvent(new ExampleBecameMoreDifficult(this.ID, this.Name));
		}
	}
}
