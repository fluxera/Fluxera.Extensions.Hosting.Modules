namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A base class for event messages that notifies that an item was added.
	/// </summary>
	[PublicAPI]
	[Serializable]
	[ExcludeFromTopology]
	public abstract class ItemAdded : IEvent
	{
		/// <summary>
		///     Gets or sets the events item added metadata.
		/// </summary>
		[Required]
		public ItemAddedData Metadata { get; internal set; }
	}
}
