namespace Fluxera.Extensions.Hosting.Modules.Domain.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;

	/// <summary>
	///     A class holding the item added metadata.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ItemAddedData
	{
		/// <summary>
		///     The ID of the entity.
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
		///     The creation timestamp.
		/// </summary>
		[Required]
		public DateTimeOffset? CreatedAt { get; set; }

		/// <summary>
		///     The (optional) user that added the entity.
		/// </summary>
		public string CreatedBy { get; set; }

		/// <summary>
		///     The state of the entity after being added in JSON format.
		/// </summary>
		[Required]
		public string AfterCreatedState { get; set; }
	}
}
