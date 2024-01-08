namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///     Marker interface to represent an event.
	/// </summary>
	[PublicAPI]
	public interface IEvent : INotification
	{
	}
}
