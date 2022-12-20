namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     The options of the repository module.
	/// </summary>
	[PublicAPI]
	public sealed class EntityFrameworkCorePersistenceOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="EntityFrameworkCorePersistenceOptions" /> type.
		/// </summary>
		public EntityFrameworkCorePersistenceOptions()
		{
			this.Repositories = new EntityFrameworkCoreRepositoryOptionsDictionary();
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///     Gets the options of the repositories.
		/// </summary>
		public EntityFrameworkCoreRepositoryOptionsDictionary Repositories { get; set; }

		/// <summary>
		///     Gets the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; set; }
	}
}
