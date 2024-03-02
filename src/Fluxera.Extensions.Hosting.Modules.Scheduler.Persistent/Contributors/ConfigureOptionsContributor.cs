namespace Fluxera.Extensions.Hosting.Modules.Scheduler.Persistent.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<PersistentSchedulerOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Scheduler:Store";
	}
}
