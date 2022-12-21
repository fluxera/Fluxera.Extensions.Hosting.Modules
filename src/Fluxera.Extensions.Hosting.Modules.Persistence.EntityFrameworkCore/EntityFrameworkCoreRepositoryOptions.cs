namespace Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options of a repository.
	/// </summary>
	[PublicAPI]
	public sealed class EntityFrameworkCoreRepositoryOptions : RepositoryOptions
	{
		/// <summary>
		///     Gets or sets the type of the db context.
		/// </summary>
		public string DbContextType { get; set; }
	}
}
