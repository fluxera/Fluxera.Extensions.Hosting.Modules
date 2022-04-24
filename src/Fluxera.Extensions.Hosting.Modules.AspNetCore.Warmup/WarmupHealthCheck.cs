namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	[UsedImplicitly]
	internal sealed class WarmupHealthCheck : IHealthCheck
	{
		private static bool isWarmedUp;

		private readonly IEnumerable<IEndpointInit> endpointInitServices;

		public WarmupHealthCheck(IEnumerable<IEndpointInit> endpointInitServices)
		{
			this.endpointInitServices = endpointInitServices;
		}

		/// <inheritdoc />
		public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
		{
			if(!isWarmedUp)
			{
				foreach(IEndpointInit endpointInit in this.endpointInitServices)
				{
					await endpointInit.InitializeEndpointsAsync();
				}

				isWarmedUp = true;
			}

			return HealthCheckResult.Healthy();
		}
	}
}
