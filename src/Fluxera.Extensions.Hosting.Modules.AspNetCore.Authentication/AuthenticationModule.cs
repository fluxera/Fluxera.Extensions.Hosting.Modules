namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables authentication.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class AuthenticationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the authentication services.
			AuthenticationBuilder builder = context.Log("AddAuthentication", services =>
			{
				IConfigurationSection section = context.Configuration.GetSection(ConfigurationSectionUtil.GetSectionName("AspNetCore:Authentication"));
				AuthenticationOptions authenticationOptions = section.Get<AuthenticationOptions>();

				return services.AddAuthentication(options =>
				{
					options.DefaultScheme = authenticationOptions.DefaultScheme;
					options.DefaultAuthenticateScheme = authenticationOptions.DefaultScheme;
					options.DefaultChallengeScheme = authenticationOptions.DefaultScheme;
				});
			});

			// Add the mvc builder.
			context.Log("AddObjectAccessor(AuthenticationBuilder)",
				services => services.AddObjectAccessor(new AuthenticationBuilderContainer(builder), ObjectAccessorLifetime.ConfigureServices));

			// Add the contributor list.
			context.Log("AddObjectAccessor(AuthenticationContributorList)",
				services => services.AddObjectAccessor(new AuthenticationContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// Configure the authentication builder.
			AuthenticationBuilderContainer container = context.Services.GetObject<AuthenticationBuilderContainer>();
			AuthenticationContributorList contributorList = context.Services.GetObject<AuthenticationContributorList>();
			foreach(IAuthenticationContributor contributor in contributorList)
			{
				contributor.Configure(container.Builder, context);
			}
		}
	}
}
