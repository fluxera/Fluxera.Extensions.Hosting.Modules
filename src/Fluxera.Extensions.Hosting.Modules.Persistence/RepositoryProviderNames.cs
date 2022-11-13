namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using JetBrains.Annotations;

	/// <summary>
	///     A class containing the supported repository providers.
	/// </summary>
	[PublicAPI]
	public static class RepositoryProviderNames
	{
		/// <summary>
		///     The in-memory repository provider.
		/// </summary>
		public const string InMemory = "InMemory";

		/// <summary>
		///     The EFCore repository provider.
		/// </summary>
		public const string EntityFrameworkCore = "EntityFrameworkCore";

		/// <summary>
		///     The LiteDB repository provider.
		/// </summary>
		public const string LiteDB = "LiteDB";

		/// <summary>
		///     The MongoDB repository provider.
		/// </summary>
		public const string MongoDB = "MongoDB";
	}
}
