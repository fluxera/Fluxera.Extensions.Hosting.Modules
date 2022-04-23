namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HealthChecks
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Diagnostics.HealthChecks;

	/// <summary>
	///     Extension methods for the <see cref="IHealthChecksBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class HealthChecksBuilderExtensions
	{
		/// <summary>
		///     Adds a health check to the builder.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="healthCheckName"></param>
		/// <param name="healthCheckType"></param>
		/// <param name="failureStatus"></param>
		/// <param name="tags"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		public static IHealthChecksBuilder AddCheck(this IHealthChecksBuilder builder, string healthCheckName, Type healthCheckType,
			HealthStatus? failureStatus = null, IEnumerable<string> tags = null)
		{
			Guard.Against.Null(builder, nameof(builder));
			Guard.Against.NullOrWhiteSpace(healthCheckName, nameof(healthCheckName));
			Guard.Against.False(healthCheckType.Implements<IHealthCheck>(), nameof(healthCheckType), $"The type must implement {nameof(IHealthCheck)}.");

			return builder.Add(new HealthCheckRegistration(healthCheckName,
				serviceProvider => (IHealthCheck)ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, healthCheckType), failureStatus, tags));
		}
	}
}
