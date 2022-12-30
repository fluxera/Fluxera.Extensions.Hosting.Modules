namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///     Defines a handler for an event.
	/// </summary>
	/// <typeparam name="TEvent">The type of event being handled.</typeparam>
	[PublicAPI]
	public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
		where TEvent : IEvent
	{
	}
}
