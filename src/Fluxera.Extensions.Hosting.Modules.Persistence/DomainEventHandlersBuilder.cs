namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using Fluxera.Repository;
	using Fluxera.Repository.DomainEvents;

	internal sealed class DomainEventHandlersBuilder : IDomainEventHandlersBuilder
	{
		private readonly IDomainEventsOptionsBuilder domainEventsOptionsBuilder;

		public DomainEventHandlersBuilder(IDomainEventsOptionsBuilder domainEventsOptionsBuilder)
		{
			this.domainEventsOptionsBuilder = domainEventsOptionsBuilder;
		}

		/// <inheritdoc />
		public IDomainEventHandlersBuilder AddDomainEventsReducer<T>() where T : class, IDomainEventsReducer
		{
			this.domainEventsOptionsBuilder.AddDomainEventsReducer<T>();
			return this;
		}
	}
}
