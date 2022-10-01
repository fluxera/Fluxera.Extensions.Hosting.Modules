namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Collections.Generic;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	internal sealed class FormattedHealthReport
	{
		private FormattedHealthReport()
		{
		}

		public HealthStatus Status { get; set; }

		public TimeSpan Duration { get; set; }

		public IList<FormattedHealthReportEntry> Entries { get; } = new List<FormattedHealthReportEntry>();

		public static FormattedHealthReport CreateFrom(HealthReport report)
		{
			FormattedHealthReport healthReport = new FormattedHealthReport
			{
				Status = report.Status,
				Duration = report.TotalDuration
			};

			foreach((string key, HealthReportEntry healthReportEntry) in report.Entries)
			{
				FormattedHealthReportEntry entry = new FormattedHealthReportEntry
				{
					Key = key,
					Description = healthReportEntry.Description,
					Status = healthReportEntry.Status,
					Duration = healthReportEntry.Duration,
					Error = healthReportEntry.Exception?.Message,
					Data = healthReportEntry.Data
				};

				healthReport.Entries.Add(entry);
			}

			return healthReport;
		}
	}
}
