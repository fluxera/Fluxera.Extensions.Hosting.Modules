namespace Fluxera.Extensions.Hosting.Modules.Scheduler.InMemory.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<InMemorySchedulerOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Scheduler";
	}
}
