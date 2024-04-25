namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration
{
	using System;
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
		public ItemRemovedData Metadata { get; internal set; }
	}
}
