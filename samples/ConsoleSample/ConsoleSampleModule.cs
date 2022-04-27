namespace ConsoleSample
{
	using ConsoleSample.Contributors;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using Fluxera.Extensions.Hosting.Modules.Messaging.InMemory;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	[DependsOn(typeof(MessagingModule))]
	[DependsOn(typeof(InMemoryMessagingModule))]
	public sealed class ConsoleSampleModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.AddConsumersContributor<ConsumersContributor>();
			context.Services.AddHostedService<ConsoleHostedService>();
		}
	}
}
