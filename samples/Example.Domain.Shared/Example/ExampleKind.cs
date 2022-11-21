namespace Example.Domain.Shared.Example
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
		///     The example is medium to follow.
		/// </summary>
		Medium,

		/// <summary>
		///     The example is hard to follow.
		/// </summary>
		Hard
	}
}
