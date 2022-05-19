namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;

	/// <summary>
	///     A class holding the item removed metadata.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ItemRemovedData
	{
		/// <summary>
		///     The ID of the added entity.
		/// </summary>
		[Required]
		public string EntityID { get; set; }

		/// <summary>
		///     The name of the entity.
		/// </summary>
		[Required]
		public string EntityName { get; set; }

		/// <summary>
		///     The long name of the entity.
		/// </summary>
		[Required]
		public string EntityLongName { get; set; }

		/// <summary>
		///     The deletion timestamp.
		/// </summary>
		[Required]
		public DateTimeOffset? DeletedAt { get; set; }

		/// <summary>
		///     The (optional) user that removed the entity.
		/// </summary>
		public string DeletedBy { get; set; }

		/// <summary>
		///     The state of the entity before being removed in JSON format.
		/// </summary>
		[Required]
		public string BeforeDeletedState { get; set; }
	}
}
