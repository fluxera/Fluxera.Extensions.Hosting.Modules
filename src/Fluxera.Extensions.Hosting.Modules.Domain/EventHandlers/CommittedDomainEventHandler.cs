namespace Fluxera.Extensions.Hosting.Modules.Domain.EventHandlers
{
	using System.Threading.Tasks;
	using Fluxera.Entity.DomainEvents;

	/// <summary>
	///     A base class for the added, updated and removed domain events.
	/// </summary>
	/// <typeparam name="TDomainEvent"></typeparam>
	public abstract class CommittedDomainEventHandler<TDomainEvent> : ICommittedDomainEventHandler<TDomainEvent>
		where TDomainEvent : class, IDomainEvent
	{
		/// <inheritdoc />
		public abstract Task HandleAsync(TDomainEvent domainEvent);
	}
}
