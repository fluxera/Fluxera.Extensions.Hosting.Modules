namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
{
	using JetBrains.Annotations;
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
			context.Log("AddAuthentication", services => services.AddAuthentication());
		}
	}
}
