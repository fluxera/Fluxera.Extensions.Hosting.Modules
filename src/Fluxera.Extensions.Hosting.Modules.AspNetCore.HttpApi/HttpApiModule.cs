namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enabled HTTP APIs.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class HttpApiModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}
	}
}
