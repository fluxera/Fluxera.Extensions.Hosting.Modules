namespace Fluxera.Extensions.Hosting.Modules.Domain.Shared.Messages
{
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     A marker interface for command types.
	/// </summary>
	[PublicAPI]
	[ExcludeFromTopology]
	public interface ICommand
	{
	}
}
