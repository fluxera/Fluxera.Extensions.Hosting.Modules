namespace Catalog.Application.Contributors
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules.Scheduler;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class SchedulerContributor : ISchedulerContributor
	{
		/// <inheritdoc />
		public void ConfigureScheduler(ISchedulerConfigurator configurator, IServiceConfigurationContext context)
		{
		}
	}
}
