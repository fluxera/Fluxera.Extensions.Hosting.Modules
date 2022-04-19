namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using JetBrains.Annotations;

	/// <summary>
	///     A class containing the supported repository providers.
	/// </summary>
	[PublicAPI]
	public static class RepositoryProviderNames
	{
		public const string InMemory = "InMemory";

		public const string EntityFrameworkCore = "EntityFrameworkCore";

		public const string LiteDB = "LiteDB";

		public const string MongoDB = "MongoDB";

		//public const string OData = "OData";
	}
}
