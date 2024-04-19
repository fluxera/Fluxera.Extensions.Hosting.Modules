namespace Fluxera.Extensions.Hosting.Modules.Domain.Messages
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A marker interface for an integration event.
	/// </summary>
	[PublicAPI]
	[ExcludeFromTopology]
	public interface IIntegrationEvent
	{
	}
}
