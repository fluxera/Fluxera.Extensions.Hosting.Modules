namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for entities that provide properties for storing creation and modification dates.
	/// </summary>
	[PublicAPI]
	public interface IAuditedObject
	{
		/// <summary>
		///     Gets or sets the created at date.
		/// </summary>
		DateTimeOffset? CreatedAt { get; set; }

		/// <summary>
		///     Gets or sets the modified at date.
		/// </summary>
		DateTimeOffset? LastModifiedAt { get; set; }

		/// <summary>
		///     Gets or sets the deleted at date.
		/// </summary>
		DateTimeOffset? DeletedAt { get; set; }

		/// <summary>
		///     Gets or sets the user id of the user that created the item.
		/// </summary>
		string CreatedBy { get; set; }

		/// <summary>
		///     Gets or sets the user id of the last user that modified the item.
		/// </summary>
		string LastModifiedBy { get; set; }

		/// <summary>
		///     Gets or sets the user id of the user that deleted the item.
		/// </summary>
		string DeletedBy { get; set; }
	}
}
