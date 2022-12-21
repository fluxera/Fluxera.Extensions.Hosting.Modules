namespace Fluxera.Extensions.Hosting.Modules.HealthChecks
{
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	[UsedImplicitly]
	internal sealed class DefaultHealthCheck : IHealthCheck
	{
		/// <inheritdoc />
		public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
		{
			return Task.FromResult(HealthCheckResult.Healthy("Default health check successful."));
		}
	}
}
