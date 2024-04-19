namespace Fluxera.Extensions.Hosting.Modules.Domain.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A base class for event messages that notifies that an item was removed.
	/// </summary>
	[PublicAPI]
	[Serializable]
	[ExcludeFromTopology]
	public abstract class ItemRemoved : IIntegrationEvent
	{
		/// <summary>
		///     Gets or sets the events item removed metadata.
		/// </summary>
		[Required]
		public ItemRemovedData Metadata { get; set; }
	}
}
