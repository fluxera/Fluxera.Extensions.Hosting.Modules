namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     The options of the repository module.
	/// </summary>
	[PublicAPI]
	public sealed class PersistenceOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="PersistenceOptions" /> type.
		/// </summary>
		public PersistenceOptions()
		{
			this.Repositories = new RepositoryOptionsDictionary();
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///     Gets the options of the repositories.
		/// </summary>
		public RepositoryOptionsDictionary Repositories { get; set; }

		/// <summary>
		///     Gets the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; set; }
	}
}
