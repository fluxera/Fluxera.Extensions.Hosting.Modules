namespace Fluxera.Extensions.Hosting.Modules.Scheduler.EntityFrameworkCore.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<EntityFrameworkCoreSchedulerOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Scheduler:Store";
	}
}
