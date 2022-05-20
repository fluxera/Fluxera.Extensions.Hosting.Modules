namespace Example.Domain.Shared.ExampleAggregate.Model
{
	using JetBrains.Annotations;

	/// <summary>
	///     An enum holding the example kinds.
	/// </summary>
	[PublicAPI]
	public enum ExampleKind
	{
		/// <summary>
		///     The example is easy to follow.
		/// </summary>
		Easy,

		/// <summary>
		///     The example is difficult to follow.
		/// </summary>
		Difficult
	}
}
