namespace Fluxera.Extensions.Hosting.Modules.Scheduler
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for scheduler configurations.
	/// </summary>
	[PublicAPI]
	public interface ISchedulerContributor
	{
		/// <summary>
		///		Configures the store the Quartz scheduler uses.
		/// </summary>
		/// <param name="configurator"></param>
		/// <param name="context"></param>
		void ConfigureScheduler(ISchedulerConfigurator configurator, IServiceConfigurationContext context);
	}
}
