namespace Fluxera.Extensions.Hosting.Modules.Messaging.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<MessagingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging";
	}
}
