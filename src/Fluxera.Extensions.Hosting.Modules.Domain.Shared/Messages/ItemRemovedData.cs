namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;

	[PublicAPI]
	[Serializable]
	public sealed class ItemRemovedData
	{
		[Required]
		public string EntityID { get; set; }

		[Required]
		public string EntityName { get; set; }

		[Required]
		public string EntityLongName { get; set; }

		[Required]
		public DateTimeOffset? DeletedAt { get; set; }

		public string DeletedBy { get; set; }

		[Required]
		public string BeforeDeletedState { get; set; }
	}
}
