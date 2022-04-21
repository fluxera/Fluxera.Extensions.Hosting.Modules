namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables JWT Bearer authentication.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AuthenticationModule))]
	public sealed class BasicAuthenticationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddBasicAuthentication", services =>
			{
				BasicAuthenticationOptions authenticationOptions = services.GetOptions<BasicAuthenticationOptions>();

				services
					.AddAuthentication(BasicDefaults.AuthenticationScheme)
					.AddBasic(options =>
					{
					});
			});
		}
	}
}
