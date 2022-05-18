namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A marker interface for event types.
	/// </summary>
	[PublicAPI]
	[ExcludeFromTopology]
	public interface IEvent
	{
	}
}
