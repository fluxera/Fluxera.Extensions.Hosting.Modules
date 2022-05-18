namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;

	[PublicAPI]
	[Serializable]
	public sealed class ItemAddedData
	{
		[Required]
		public string EntityID { get; set; }

		[Required]
		public string EntityName { get; set; }

		[Required]
		public string EntityLongName { get; set; }

		[Required]
		public DateTimeOffset? CreatedAt { get; set; }

		public string CreatedBy { get; set; }

		[Required]
		public string AfterCreatedState { get; set; }
	}
}
