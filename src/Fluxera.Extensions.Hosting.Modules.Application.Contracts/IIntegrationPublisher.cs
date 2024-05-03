namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for publishing integration events.
	/// </summary>
	[PublicAPI]
	public interface IIntegrationPublisher
	{
		/// <summary>
		///		Publish the given integration event.
		/// </summary>
		/// <typeparam name="TEvent"></typeparam>
		/// <param name="event"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
			where TEvent : class, IIntegrationEvent;
	}
}
