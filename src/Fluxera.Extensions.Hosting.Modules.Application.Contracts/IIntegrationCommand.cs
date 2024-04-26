namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A marker interface for an integration command.
	/// </summary>
	[PublicAPI]
	[ExcludeFromTopology]
	public interface IIntegrationCommand
	{
	}
}
