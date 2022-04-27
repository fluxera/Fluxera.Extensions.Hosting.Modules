namespace Fluxera.Extensions.Hosting.Modules.Messaging.RabbitMQ.Contributors
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<RabbitMqMessagingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, RabbitMqMessagingOptions createdOptions)
		{
			createdOptions.ConnectionStrings = context.Services.GetOptions<ConnectionStrings>();

			context.Log("Configure(RabbitMqMessagingOptions)", services =>
			{
				services.Configure<RabbitMqMessagingOptions>(options =>
				{
					options.ConnectionStrings = createdOptions.ConnectionStrings;
				});
			});
		}
	}
}
