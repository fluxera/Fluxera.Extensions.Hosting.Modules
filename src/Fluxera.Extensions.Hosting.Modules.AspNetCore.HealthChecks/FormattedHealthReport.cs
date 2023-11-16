namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	/// <summary>
	///		A formatted health report.
	/// </summary>
	[PublicAPI]
	public sealed class FormattedHealthReport
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="FormattedHealthReport"/> type.
		/// </summary>
		public FormattedHealthReport()
		{
			this.Entries = new Dictionary<string, FormattedHealthReportEntry>();
		}

		/// <summary>
		///		Gets or sets the status.
		/// </summary>
		/// <remarks>
		///		See: <see cref="HealthReport.Status"/>
		/// </remarks>
		public HealthStatus Status { get; set; }

		/// <summary>
		///		Gets or sets the total duration.
		/// </summary>
		/// <remarks>
		///		See: <see cref="HealthReport.TotalDuration"/>
		/// </remarks>
		public TimeSpan TotalDuration { get; set; }

		/// <summary>
		///		Gets or sets the report entries.
		/// </summary>
		/// <remarks>
		///		See: <see cref="HealthReport.Entries"/>
		/// </remarks>
		public IDictionary<string, FormattedHealthReportEntry> Entries { get; set; }

		internal static FormattedHealthReport CreateFrom(HealthReport report)
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
					Description = healthReportEntry.Description ?? string.Empty,
					Status = healthReportEntry.Status,
					Duration = healthReportEntry.Duration,
					Data = healthReportEntry.Data.ToDictionary(x => x.Key, x => x.Value)
				};

				if(healthReportEntry.Exception is not null)
				{
					entry.Exception = healthReportEntry.Exception?.Message ?? string.Empty;
					entry.Description = healthReportEntry.Description ?? healthReportEntry.Exception?.Message ?? string.Empty;
				}

				entry.Tags = healthReportEntry.Tags.ToList();

				healthReport.Entries.Add(key, entry);
			}

			return healthReport;
		}
	}
}
