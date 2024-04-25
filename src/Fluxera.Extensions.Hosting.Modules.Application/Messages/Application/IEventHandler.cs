namespace Fluxera.Extensions.Hosting.Modules.Application.Messages.Application
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Application;
	using JetBrains.Annotations;
	using MediatR;

	/// <summary>
	///		A contract for application event handlers.
	/// </summary>
	/// <typeparam name="TEvent"></typeparam>
	[PublicAPI]
	public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
		where TEvent : class, IEvent
	{
		/// <summary>
		///		Handles an application event.
		/// </summary>
		/// <param name="event"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task HandleAsync(TEvent @event, CancellationToken cancellationToken);

		/// <inheritdoc />
		Task INotificationHandler<TEvent>.Handle(TEvent @event, CancellationToken cancellationToken)
		{
			return this.HandleAsync(@event, cancellationToken);
		}
	}
}
