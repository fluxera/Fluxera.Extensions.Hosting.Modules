namespace Fluxera.Extensions.Hosting.Modules.Scheduler.MongoDB.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<MongoSchedulerOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Scheduler:Store";
	}
}
