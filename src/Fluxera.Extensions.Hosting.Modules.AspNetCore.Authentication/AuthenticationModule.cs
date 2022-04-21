namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authentication;
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
				AuthenticationOptions authenticationOptions = services.GetOptions<AuthenticationOptions>();
				return services.AddAuthentication(authenticationOptions.DefaultScheme);
			});

			// Add the mvc builder.
			context.Log("AddObjectAccessor(AuthenticationBuilder)",
				services => services.AddObjectAccessor(builder, ObjectAccessorLifetime.ConfigureServices));

			// Add the contributor list.
			context.Log("AddObjectAccessor(AuthenticationContributorList)",
				services => services.AddObjectAccessor(new AuthenticationContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// Configure the authentication builder.
			AuthenticationBuilder builder = context.Services.GetObject<AuthenticationBuilder>();
			AuthenticationContributorList contributorList = context.Services.GetObject<AuthenticationContributorList>();
			foreach(IAuthenticationContributor contributor in contributorList)
			{
				contributor.Configure(builder, context);
			}
		}
	}
}
