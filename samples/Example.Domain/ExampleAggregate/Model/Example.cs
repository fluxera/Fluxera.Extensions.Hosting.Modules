namespace Example.Domain.ExampleAggregate.Model
{
	using Fluxera.Entity;
	using global::Example.Domain.Shared.ExampleAggregate.Model;
	using JetBrains.Annotations;

	/// <summary>
	///     An aggregate root holding the information of an example.
	/// </summary>
	/// <seealso cref="AggregateRoot{TAggregateRoot,TKey}" />
	[PublicAPI]
	public sealed class Example : AggregateRoot<Example, string>
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
	}
}
