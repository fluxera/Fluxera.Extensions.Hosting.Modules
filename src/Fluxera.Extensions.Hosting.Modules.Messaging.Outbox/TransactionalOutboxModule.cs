namespace Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.TransactionalOutbox.Contributors;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables the transactional outbox.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(MessagingModule))]
	public sealed class TransactionalOutboxModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			context.Items.Add("IsTransactionalOutboxModuleLoaded", true);

			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Disable the cleanup and delivery services if the multi tenancy module is loaded.
			bool isMultiTenancyModuleLoaded = context.Items.ContainsKey("IsMultiTenancyModuleLoaded");
			if(isMultiTenancyModuleLoaded)
			{
				TransactionalOutboxModuleOptions options = context.Services.GetOptions<TransactionalOutboxModuleOptions>();
				options.InboxCleanupServiceEnabled = false;
				options.DeliveryServiceEnabled = false;
			}
		}
	}
}
