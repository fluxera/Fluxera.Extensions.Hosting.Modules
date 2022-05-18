namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;
	using MassTransit;

	[PublicAPI]
	[Serializable]
	[ExcludeFromTopology]
	public abstract class ItemUpdated
	{
		[Required]
		public ItemUpdatedData Metadata { get; set; }
	}
}
