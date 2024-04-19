namespace Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers
{
	using System.Threading.Tasks;
	using Fluxera.Entity.DomainEvents;

	/// <summary>
	///     A base class handling domain events.
	/// </summary>
	/// <typeparam name="TDomainEvent"></typeparam>
	public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
		where TDomainEvent : class, IDomainEvent
	{
		/// <inheritdoc />
		public abstract Task HandleAsync(TDomainEvent domainEvent);
	}
}
