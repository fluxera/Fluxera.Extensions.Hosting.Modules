namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	/// <summary>
	///     A descriptor of a health check implementation.
	/// </summary>
	[PublicAPI]
	public sealed class HealthCheckDescriptor
	{
		/// <summary>
		///     Creates a new instance of the <see cref="HealthCheckDescriptor" /> type.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="name"></param>
		/// <param name="category"></param>
		public HealthCheckDescriptor(Type type, string name, HealthCheckCategory category)
		{
			this.Type = type;
			this.Name = name;
			this.Category = category;
		}

		/// <summary>
		///     The type of the health check class, that implements <see cref="IHealthCheck" />.
		/// </summary>
		public Type Type { get; }

		/// <summary>
		///     The name of the health check.
		/// </summary>
		public string Name { get; }

		/// <summary>
		///     The category of this health check.
		/// </summary>
		public HealthCheckCategory Category { get; }
	}
}
