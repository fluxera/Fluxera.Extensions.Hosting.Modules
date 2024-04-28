namespace Fluxera.Extensions.Hosting.Modules.Application.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using global::MediatR;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for application event handlers.
	/// </summary>
	/// <typeparam name="TEvent"></typeparam>
	[PublicAPI]
	public interface IApplicationEventHandler<in TEvent> : INotificationHandler<TEvent>
		where TEvent : class, IApplicationEvent
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
