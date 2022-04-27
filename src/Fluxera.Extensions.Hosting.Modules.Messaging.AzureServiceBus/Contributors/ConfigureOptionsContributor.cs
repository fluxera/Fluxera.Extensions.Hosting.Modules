namespace Fluxera.Extensions.Hosting.Modules.Messaging.AzureServiceBus.Contributors
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<AzureServiceBusMessagingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, AzureServiceBusMessagingOptions createdOptions)
		{
			createdOptions.ConnectionStrings = context.Services.GetOptions<ConnectionStrings>();

			context.Log("Configure(AzureServiceBusMessagingOptions)", services =>
			{
				services.Configure<AzureServiceBusMessagingOptions>(options =>
				{
					options.ConnectionStrings = createdOptions.ConnectionStrings;
				});
			});
		}
	}
}
