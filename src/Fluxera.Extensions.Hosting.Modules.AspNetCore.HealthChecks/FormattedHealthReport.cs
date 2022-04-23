namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Collections.Generic;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	internal sealed class FormattedHealthReport
	{
		private FormattedHealthReport(Dictionary<string, FormattedHealthReportEntry> entries, TimeSpan totalDuration)
		{
			this.Entries = entries;
			this.TotalDuration = totalDuration;
		}

		public HealthStatus Status { get; set; }

		public TimeSpan TotalDuration { get; set; }

		public Dictionary<string, FormattedHealthReportEntry> Entries { get; }

		public static FormattedHealthReport CreateFrom(HealthReport report)
		{
			FormattedHealthReport healthReport =
				new FormattedHealthReport(new Dictionary<string, FormattedHealthReportEntry>(), report.TotalDuration)
				{
					Status = report.Status,
				};

			foreach((string key, HealthReportEntry healthReportEntry) in report.Entries)
			{
				FormattedHealthReportEntry entry = new FormattedHealthReportEntry
				{
					Data = healthReportEntry.Data,
					Description = healthReportEntry.Description,
					Duration = healthReportEntry.Duration,
					Status = healthReportEntry.Status
				};

				if(healthReportEntry.Exception != null)
				{
					string message = healthReportEntry.Exception?.Message;

					entry.Exception = message;
					entry.Description = healthReportEntry.Description ?? message;
				}

				healthReport.Entries.Add(key, entry);
			}

			return healthReport;
		}

		public static FormattedHealthReport CreateFrom(Exception exception, string entryName = "Endpoint")
		{
			FormattedHealthReport healthReport =
				new FormattedHealthReport(new Dictionary<string, FormattedHealthReportEntry>(), TimeSpan.FromSeconds(0))
				{
					Status = HealthStatus.Unhealthy,
				};

			healthReport.Entries.Add(entryName, new FormattedHealthReportEntry
			{
				Exception = exception.Message,
				Description = exception.Message,
				Duration = TimeSpan.FromSeconds(0),
				Status = HealthStatus.Unhealthy
			});

			return healthReport;
		}
	}
}
