namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Collections.Generic;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	internal sealed class FormattedHealthReportEntry
	{
		public string Key { get; set; }

		public string Description { get; set; }

		public HealthStatus Status { get; set; }

		public TimeSpan Duration { get; set; }

		public string Error { get; set; }

		public IReadOnlyDictionary<string, object> Data { get; set; }
	}
}
