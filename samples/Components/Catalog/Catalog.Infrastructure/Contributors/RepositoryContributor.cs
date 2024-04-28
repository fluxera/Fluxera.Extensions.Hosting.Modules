namespace Catalog.Infrastructure.Contributors
{
	using Catalog.Domain;
	using Catalog.Domain.Products;
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
		public override void ConfigureValidators(IValidatorsBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddValidatorsFromAssembly(CatalogDomain.Assembly);
		}

		/// <inheritdoc />
		public override void ConfigureInterceptors(IInterceptorsBuilder builder, IServiceConfigurationContext context)
		{
			builder.AddInterceptorsFromAssembly(CatalogInfrastructure.Assembly);
		}
	}
}
