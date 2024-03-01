namespace Fluxera.Extensions.Hosting.Modules.Messaging.AmazonSQS.Contributors
{
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<AmazonSqsMessagingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, AmazonSqsMessagingOptions createdOptions)
		{
			createdOptions.ConnectionStrings = context.Services.GetOptions<ConnectionStrings>();

			context.Log("Configure(AmazonSqsMessagingOptions)", services =>
			{
				services.Configure<AmazonSqsMessagingOptions>(options =>
				{
					options.ConnectionStrings = createdOptions.ConnectionStrings;
				});
			});
		}
	}
}
