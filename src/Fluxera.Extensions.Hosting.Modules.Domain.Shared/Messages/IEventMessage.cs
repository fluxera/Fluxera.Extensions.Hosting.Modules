﻿namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A marker interface for an integration event message.
	/// </summary>
	[PublicAPI]
	[ExcludeFromTopology]
	public interface IEventMessage
	{
	}
}
