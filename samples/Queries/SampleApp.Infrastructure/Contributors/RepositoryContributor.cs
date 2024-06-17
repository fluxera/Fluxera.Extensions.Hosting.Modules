namespace SampleApp.Infrastructure.Contributors
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using JetBrains.Annotations;
	using SampleApp.Domain;
	using SampleApp.Domain.Customers;

	[UsedImplicitly]
	internal sealed class RepositoryContributor : RepositoryContributorBase
	{
		/// <inheritdoc />
		public override void ConfigureAggregates(IRepositoryAggregatesBuilder builder, IServiceConfigurationContext context)
		{
			// Configure the aggregates for this repository.
			builder.UseFor<Customer>();
		}

		/// <inheritdoc />
		public override void ConfigureValidators(IValidatorsBuilder builder, IServiceConfigurationContext context)
		{
			// Add the domain entities validators.
			builder.AddValidatorsFromAssembly(SampleAppDomain.Assembly);
		}

		/// <inheritdoc />
		public override void ConfigureInterceptors(IInterceptorsBuilder builder, IServiceConfigurationContext context)
		{
			// Add the repository interceptors.
			builder.AddInterceptorsFromAssembly(SampleAppInfrastructure.Assembly);
		}
	}
}
