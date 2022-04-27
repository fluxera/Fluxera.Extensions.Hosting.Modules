namespace Fluxera.Extensions.Hosting.Modules.Messaging.InMemory.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<InMemoryMessagingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging";
	}
}
