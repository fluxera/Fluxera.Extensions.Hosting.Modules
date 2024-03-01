namespace Fluxera.Extensions.Hosting.Modules.Messaging.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using MassTransit;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<MessagingOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Messaging";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, MessagingOptions createdOptions)
		{
			context.Log("Configure(MassTransitHostOptions)", services =>
			{
				services.Configure<MassTransitHostOptions>(options =>
				{
					options.WaitUntilStarted = createdOptions.WaitUntilStarted;
					options.StartTimeout = createdOptions.StartTimeout;
					options.StopTimeout = createdOptions.StopTimeout;
					options.ConsumerStopTimeout = createdOptions.ConsumerStopTimeout;
				});
			});
		}
	}
}
