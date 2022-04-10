namespace Fluxera.Extensions.Hosting.Modules.Principal
{
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Principal.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables an accessor for the current user.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class PrincipalModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the principal as service using the principal accessor.
			context.Log("AddPrincipalAccessor",
				services => services.AddPrincipalAccessor());

			// Add the thread as source for a principal.
			context.Log("AddPrincipalProvider(Thread)",
				services => services.AddPrincipalProvider<ThreadPrincipalProvider>());
		}
	}
}
