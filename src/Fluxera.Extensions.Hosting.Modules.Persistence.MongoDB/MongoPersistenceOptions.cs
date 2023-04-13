namespace Fluxera.Extensions.Hosting.Modules.Persistence.MongoDB
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     The options of the repository module.
	/// </summary>
	[PublicAPI]
	public sealed class MongoPersistenceOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="MongoPersistenceOptions" /> type.
		/// </summary>
		public MongoPersistenceOptions()
		{
			this.Repositories = new MongoRepositoryOptionsDictionary();
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///     Gets the options of the repositories.
		/// </summary>
		public MongoRepositoryOptionsDictionary Repositories { get; set; }

		/// <summary>
		///     Gets the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; set; }
	}
}
