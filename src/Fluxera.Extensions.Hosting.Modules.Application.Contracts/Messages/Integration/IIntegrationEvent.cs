namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration
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
