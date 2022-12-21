namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Collections.Generic;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	internal sealed class FormattedHealthReportEntry
	{
		public string Description { get; set; }

		public HealthStatus Status { get; set; }

		public TimeSpan Duration { get; set; }

		public string Exception { get; set; }

		public IReadOnlyDictionary<string, object> Data { get; set; }

		public IReadOnlyCollection<string> Tags { get; set; }
	}
}
