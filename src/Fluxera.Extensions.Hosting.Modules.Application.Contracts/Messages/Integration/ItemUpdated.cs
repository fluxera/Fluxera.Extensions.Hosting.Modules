namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration
{
	using System;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A base class for event messages that notifies that an item was updated.
	/// </summary>
	[PublicAPI]
	[Serializable]
	[ExcludeFromTopology]
	public abstract class ItemUpdated : IIntegrationEvent
	{
		/// <summary>
		///     Gets or sets the events item updated metadata.
		/// </summary>
		public ItemUpdatedData Metadata { get; internal set; }
	}
}
