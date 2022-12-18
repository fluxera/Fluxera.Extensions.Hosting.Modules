namespace Catalog.Infrastructure.Contributors
{
	using Catalog.Domain.Product;
	using Catalog.Domain.Product.Validation;
	using Catalog.Infrastructure.Product.Handlers;
	using Catalog.Infrastructure.Product.Interceptors;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class RepositoryContributor : IRepositoryContributor
	{
		/// <inheritdoc />
		public void ConfigureAggregates(IRepositoryAggregatesBuilder builder, IServiceConfigurationContext context)
		{
			builder.UseFor<Product>();
		}

		/// <inheritdoc />
		public void ConfigureDomainEventHandlers(IDomainEventHandlersBuilder builder, IServiceConfigurationContext context)
		{
			builder
				.AddDomainEventHandler<ProductAddedHandler>()
				.AddDomainEventHandler<ProductUpdatedHandler>()
				.AddDomainEventHandler<ProductRemovedHandler>();
		}

		/// <inheritdoc />
		public void ConfigureValidators(IValidatorsBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddValidator<ProductValidator>();
		}

		/// <inheritdoc />
		public void ConfigureInterceptors(IInterceptorsBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddInterceptor<ProductInterceptor>();
		}

		/// <inheritdoc />
		public void ConfigureCaching(ICachingBuilder builder, IServiceConfigurationContext context)
		{
			builder.UseNoCaching();
		}
	}
}
