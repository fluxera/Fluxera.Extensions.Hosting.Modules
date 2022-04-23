namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System.Collections.Generic;

	internal sealed class HealthCheckContributorList : List<IHealthCheckContributor>
	{
	}
}
