namespace Fluxera.Extensions.Hosting.Modules.Messaging.Scheduler
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.Scheduler.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables scheduler support.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(MessagingModule))]
	public sealed class SchedulerMessagingModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the consumer contributor.
			context.Services.AddConsumersContributor<ConsumersContributor>();
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			//bool isRegistered = context.Services.IsRegistered<ISchedulerFactory>();
			//if(!isRegistered)
			//{
			//	throw new InvalidOperationException("The scheduler was not configured.");
			//}
		}
	}
}
