namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration
{
	using System;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A base class for event messages that notifies that an item was added.
	/// </summary>
	[PublicAPI]
	[Serializable]
	[ExcludeFromTopology]
	public abstract class ItemAdded : IIntegrationEvent
	{
		/// <summary>
		///     Gets or sets the events item added metadata.
		/// </summary>
		public ItemAddedData Metadata { get; internal set; }
	}
}
