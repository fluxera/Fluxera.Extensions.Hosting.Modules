namespace Fluxera.Extensions.Hosting.Modules.UnitTesting
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Hosting.Internal;

	[PublicAPI]
	public abstract class StartupModuleTestBase<TStartupModule> : TestBase
		where TStartupModule : class, IModule
	{
		protected IApplicationLoader ApplicationLoader { get; private set; }

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
