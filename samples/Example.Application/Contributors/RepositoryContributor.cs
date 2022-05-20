namespace Example.Application.Contributors
{
	using Example.Domain.ExampleAggregate.EventHandlers;
	using Example.Domain.ExampleAggregate.Interceptors;
	using Example.Domain.ExampleAggregate.Model;
	using Example.Domain.ExampleAggregate.Validation;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Persistence;

	internal sealed class RepositoryContributor : IRepositoryContributor
	{
		/// <inheritdoc />
		public void ConfigureAggregates(IRepositoryAggregatesBuilder builder, IServiceConfigurationContext context)
		{
			builder.UseFor<Example>();
		}

		/// <inheritdoc />
		public void ConfigureDomainEventHandlers(IDomainEventHandlersBuilder builder, IServiceConfigurationContext context)
		{
			builder
				.AddDomainEventHandler<ExampleAddedHandler>()
				.AddDomainEventHandler<ExampleUpdatedHandler>()
				.AddDomainEventHandler<ExampleRemovedHandler>();
		}

		/// <inheritdoc />
		public void ConfigureValidators(IValidatorsBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddValidator<ExampleValidator>();
		}

		/// <inheritdoc />
		public void ConfigureInterceptors(IInterceptorsBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddInterceptor<ExampleInterceptor>();
		}

		/// <inheritdoc />
		public void ConfigureCaching(ICachingBuilder builder, IServiceConfigurationContext context)
		{
			builder.UseNoCaching();
		}
	}
}
