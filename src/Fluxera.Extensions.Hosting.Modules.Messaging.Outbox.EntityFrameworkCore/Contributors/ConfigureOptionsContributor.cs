namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.EntityFrameworkCore.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<EntityFrameworkCoreTransactionalOutboxModuleOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging";
	}
}
