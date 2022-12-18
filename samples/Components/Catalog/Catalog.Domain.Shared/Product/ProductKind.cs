namespace Catalog.Domain.Shared.Product
{
	using JetBrains.Annotations;

	/// <summary>
	///     An enum holding the example kinds.
	/// </summary>
	[PublicAPI]
	public enum ProductKind
	{
		/// <summary>
		///     The example is easy to follow.
		/// </summary>
		ConsumerGoods,

		/// <summary>
		///     The example is medium to follow.
		/// </summary>
		Groceries,

		/// <summary>
		///     The example is hard to follow.
		/// </summary>
		LuxuryGoods
	}
}
