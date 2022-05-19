namespace Fluxera.Extensions.Hosting.Modules.UnitTesting
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Hosting.Internal;

	/// <summary>
	///     A base class for testing modules.
	/// </summary>
	/// <typeparam name="TStartupModule"></typeparam>
	[PublicAPI]
	public abstract class StartupModuleTestBase<TStartupModule> : TestBase
		where TStartupModule : class, IModule
	{
		/// <summary>
		///     Gets or sets the application loader instance.
		/// </summary>
		protected IApplicationLoader ApplicationLoader { get; private set; }

		/// <summary>
		///     Starts the application.
		/// </summary>
		protected void StartApplication()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				IConfiguration configuration = new ConfigurationBuilder()
					.AddJsonFile("appsettings.json")
					.Build();
				IHostEnvironment environment = new HostingEnvironment
				{
					EnvironmentName = "Development",
					ApplicationName = "UnitTests"
				};

				services.AddSingleton(environment);
				services.AddSingleton<IHostLifetime, TestLifetime>();
				services.AddSingleton<IHostApplicationLifetime, TestApplicationLifetime>();

				services.AddApplicationLoader<TStartupModule>(configuration, environment, CreateBootstrapperLogger());
			});

			this.ApplicationLoader = serviceProvider.GetRequiredService<IApplicationLoader>();
			this.ApplicationLoader.Initialize(new ApplicationLoaderInitializationContext(serviceProvider));
		}

		/// <summary>
		///     Stops the application.
		/// </summary>
		protected void StopApplication()
		{
			if(this.ApplicationLoader != null)
			{
				this.ApplicationLoader.Shutdown();
				this.ApplicationLoader = null;
			}
		}
	}
}
