namespace Example.Domain.Example
{
	using Fluxera.ValueObject;
	using JetBrains.Annotations;

	/// <summary>
	///     A value object representing an example detail.
	/// </summary>
	/// <seealso cref="ValueObject{TValueObject}" />
	[PublicAPI]
	public sealed class ExampleDetail : ValueObject<ExampleDetail>
	{
		/// <summary>
		///     Gets or sets the title of the example detail.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///     Gets or sets the text of the example detail.
		/// </summary>
		public string Text { get; set; }
	}
}
