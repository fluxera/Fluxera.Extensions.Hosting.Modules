namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	/// <summary>
	///		A formatted health report entry.
	/// </summary>
	[PublicAPI]
	public sealed class FormattedHealthReportEntry
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="FormattedHealthReportEntry"/> type.
		/// </summary>
		public FormattedHealthReportEntry()
		{
			this.Data = new Dictionary<string, object>();
			this.Tags = new List<string>();
		}

		/// <summary>
		///		Gets or sets the entry description.
		/// </summary>
		/// <remarks>
		///		See: <see cref="HealthReportEntry.Description"/>
		/// </remarks>
		public string Description { get; set; }

		/// <summary>
		///		Gets or sets the status.
		/// </summary>
		/// <remarks>
		///		See: <see cref="HealthReportEntry.Status"/>
		/// </remarks>
		public HealthStatus Status { get; set; }

		/// <summary>
		///		Gets or sets the duration.
		/// </summary>
		/// <remarks>
		///		See: <see cref="HealthReportEntry.Duration"/>
		/// </remarks>
		public TimeSpan Duration { get; set; }

		/// <summary>
		///		Gets or sets the exception message.
		/// </summary>
		/// <remarks>
		///		See: <see cref="HealthReportEntry.Exception"/>
		/// </remarks>
		public string Exception { get; set; }

		/// <summary>
		///		Gets or sets the data.
		/// </summary>
		/// <remarks>
		///		See: <see cref="HealthReportEntry.Data"/>
		/// </remarks>
		public IDictionary<string, object> Data { get; set; }

		/// <summary>
		///		Gets or sets the tags.
		/// </summary>
		/// <remarks>
		///		See: <see cref="HealthReportEntry.Tags"/>
		/// </remarks>
		public ICollection<string> Tags { get; set; }
	}
}
