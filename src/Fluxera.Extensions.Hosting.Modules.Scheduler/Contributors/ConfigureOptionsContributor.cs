namespace Fluxera.Extensions.Hosting.Modules.Scheduler.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<SchedulerOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Scheduler";
	}
}
