﻿namespace Fluxera.Extensions.Hosting.Modules.Scheduler
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for storage specific configurations.
	/// </summary>
	[PublicAPI]
	public interface IStoreContributor
	{
		/// <summary>
		///		Configures the store the Quartz scheduler uses.
		/// </summary>
		/// <param name="configurator"></param>
		/// <param name="context"></param>
		void ConfigureStore(ISchedulerStoreConfigurator configurator, IServiceConfigurationContext context);
	}
}
