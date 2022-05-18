namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;
	using MassTransit;

	[PublicAPI]
	[Serializable]
	[ExcludeFromTopology]
	public abstract class ItemRemoved
	{
		[Required]
		public ItemRemovedData Metadata { get; set; }
	}
}
