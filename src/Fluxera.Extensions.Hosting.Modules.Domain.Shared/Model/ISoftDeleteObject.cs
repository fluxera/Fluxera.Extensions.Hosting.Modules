namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model
{
	using JetBrains.Annotations;

	/// <summary>
	///     Used to standardize soft deleting entities. A soft-deleted entity is not
	///     actually deleted, but marked as <c>IsDeleted = true</c> in the database.
	///     It can not be retrieved to the application normally.
	/// </summary>
	[PublicAPI]
	public interface ISoftDeleteObject
	{
		/// <summary>
		///     Used to mark an entity as deleted.
		/// </summary>
		bool IsDeleted { get; set; }
	}
}
