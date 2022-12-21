namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	internal sealed class FormattedHealthReport
	{
		private FormattedHealthReport()
		{
		}

		public HealthStatus Status { get; set; }

		public TimeSpan TotalDuration { get; set; }

		public IDictionary<string, FormattedHealthReportEntry> Entries { get; } = new Dictionary<string, FormattedHealthReportEntry>();

		public static FormattedHealthReport CreateFrom(HealthReport report)
		{
			FormattedHealthReport healthReport = new FormattedHealthReport
			{
				Status = report.Status,
				TotalDuration = report.TotalDuration
			};

			foreach((string key, HealthReportEntry healthReportEntry) in report.Entries)
			{
				FormattedHealthReportEntry entry = new FormattedHealthReportEntry
				{
					Description = healthReportEntry.Description,
					Status = healthReportEntry.Status,
					Duration = healthReportEntry.Duration,
					Data = healthReportEntry.Data
				};

				if(healthReportEntry.Exception is not null)
				{
					entry.Exception = healthReportEntry.Exception?.Message;
					entry.Description = healthReportEntry.Description ?? healthReportEntry.Exception?.Message;
				}

				entry.Tags = healthReportEntry.Tags.AsReadOnly();

				healthReport.Entries.Add(key, entry);
			}

			return healthReport;
		}
	}
}
