namespace Catalog.Infrastructure.Contributors
{
	using Catalog.Domain.ProductAggregate;
	using Catalog.Domain.ProductAggregate.Validation;
	using Catalog.Infrastructure.ProductAggregate.Handlers;
	using Catalog.Infrastructure.ProductAggregate.Interceptors;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class RepositoryContributor : RepositoryContributorBase
	{
		/// <inheritdoc />
		public override void ConfigureAggregates(IRepositoryAggregatesBuilder builder, IServiceConfigurationContext context)
		{
			builder.UseFor<Product>();
		}

		/// <inheritdoc />
		public override void ConfigureDomainEventHandlers(IDomainEventHandlersBuilder builder, IServiceConfigurationContext context)
		{
			builder
				.AddDomainEventHandler<ProductAddedHandler>()
				.AddDomainEventHandler<ProductUpdatedHandler>()
				.AddDomainEventHandler<ProductRemovedHandler>();
		}

		/// <inheritdoc />
		public override void ConfigureValidators(IValidatorsBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddValidator<ProductValidator>();
		}

		/// <inheritdoc />
		public override void ConfigureInterceptors(IInterceptorsBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddInterceptor<ProductInterceptor>();
		}
	}
}
