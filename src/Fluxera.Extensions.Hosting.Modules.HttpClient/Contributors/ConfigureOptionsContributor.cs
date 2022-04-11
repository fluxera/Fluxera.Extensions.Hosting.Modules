namespace Fluxera.Extensions.Hosting.Modules.HttpClient.Contributors
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Http;
	using Fluxera.Utilities.Extensions;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class ConfigureOptionsContributor : ConfigureOptionsContributorBase<HttpClientOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "HttpClient";

		/// <inheritdoc />
		protected override void AdditionalConfigure(IServiceConfigurationContext context, HttpClientOptions createdOptions)
		{
			context.Log("Configure(RemoteServiceOptions)",
				services => services.Configure<RemoteServiceOptions>(options =>
				{
					options.RemoteServices.AddRange(createdOptions.RemoteServices);
				}));
		}
	}
}
