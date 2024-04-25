namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A class holding the item updated metadata.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ItemUpdatedData
	{
		/// <summary>
		///     The ID of the entity.
		/// </summary>
		public string EntityID { get; set; }

		/// <summary>
		///     The name of the entity.
		/// </summary>
		public string EntityName { get; set; }

		/// <summary>
		///     The long name of the entity.
		/// </summary>
		public string EntityLongName { get; set; }

		/// <summary>
		///     The update timestamp.
		/// </summary>
		public DateTimeOffset? LastModifiedAt { get; set; }

		/// <summary>
		///     The (optional) user that updated the entity.
		/// </summary>
		public string LastModifiedBy { get; set; }

		/// <summary>
		///     The state of the entity before being updated in JSON format.
		/// </summary>
		public string BeforeUpdateState { get; set; }

		/// <summary>
		///     The state of the entity after being updated in JSON format.
		/// </summary>
		public string AfterUpdateState { get; set; }

		/// <summary>
		///     The changes made to the entity.
		/// </summary>
		public IList<Change> Changes { get; set; }
	}
}
