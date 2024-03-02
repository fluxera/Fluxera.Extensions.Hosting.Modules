namespace Fluxera.Extensions.Hosting.Modules.Scheduler.MongoDB
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Scheduler;
	using Fluxera.Extensions.Hosting.Modules.Scheduler.MongoDB.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables in-memory storage for the Quartz scheduler.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(SchedulerModule))]
	public sealed class MongoSchedulerModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the store contributor.
			context.Services.AddStoreContributor<StoreContributor>();
		}
	}
}
