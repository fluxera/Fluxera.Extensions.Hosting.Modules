namespace Fluxera.Extensions.Hosting.Modules.UnitTesting
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     A base class for tests.
	/// </summary>
	[PublicAPI]
	public abstract class TestBase
	{
		/// <summary>
		///     Builds the service provider with the given services.
		/// </summary>
		/// <param name="configure"></param>
		/// <returns></returns>
		protected static IServiceProvider BuildServiceProvider(Action<IServiceCollection> configure)
		{
			IServiceCollection services = new ServiceCollection();

			services.AddLogging(builder =>
			{
				builder.SetMinimumLevel(LogLevel.Trace);
				builder.AddConsole();
			});

			configure(services);
			return services.BuildServiceProvider();
		}

		/// <summary>
		///     Creates a bootstrapper logger for the application tests.
		/// </summary>
		/// <returns></returns>
		protected static ILogger CreateBootstrapperLogger()
		{
			ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
			{
				builder.SetMinimumLevel(LogLevel.Trace);
				builder.AddConsole();
			});

			return loggerFactory.CreateLogger(ApplicationHost.LoggerName);
		}
	}
}
