namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.EntityFrameworkCore.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<TransactionalOutboxModuleOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging";
	}
}
