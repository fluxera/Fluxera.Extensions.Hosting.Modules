namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;

	[PublicAPI]
	[Serializable]
	public sealed class ItemUpdatedData
	{
		[Required]
		public string EntityID { get; set; }

		[Required]
		public string EntityName { get; set; }

		[Required]
		public string EntityLongName { get; set; }

		[Required]
		public DateTimeOffset? LastModifiedAt { get; set; }

		public string LastModifiedBy { get; set; }

		[Required]
		public string BeforeUpdateState { get; set; }

		[Required]
		public string AfterUpdateState { get; set; }

		[Required]
		public IList<Change> Changes { get; set; }
	}
}
