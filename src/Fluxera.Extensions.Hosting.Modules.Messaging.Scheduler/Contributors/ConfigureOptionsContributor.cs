namespace Fluxera.Extensions.Hosting.Modules.Messaging.Scheduler.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<SchedulerMessagingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging:Scheduler";
	}
}
