namespace Fluxera.Extensions.Hosting.Modules.Messaging.AmazonSQS.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.HealthChecks;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class HealthChecksContributor : IHealthChecksContributor
	{
		/// <inheritdoc />
		public void ConfigureHealthChecks(IHealthChecksBuilder builder, IServiceConfigurationContext context)
		{
		}
	}
}
