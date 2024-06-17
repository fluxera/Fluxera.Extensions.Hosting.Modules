namespace SampleApp.Domain
{
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using JetBrains.Annotations;

	/// <summary>
	///     The domain module.
	/// </summary>
	[PublicAPI]
	[DependsOn<DomainModule>]
	public sealed class SampleAppDomainModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add domain services.
			context.Log("AddGuidGenerator", services => services.AddGuidGenerator());
		}
	}
}
