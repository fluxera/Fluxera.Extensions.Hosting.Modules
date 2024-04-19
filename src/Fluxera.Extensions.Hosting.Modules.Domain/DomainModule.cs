namespace Fluxera.Extensions.Hosting.Modules.Domain
{
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;

	/// <summary>
	///     A module that enables the domain.
	/// </summary>
	[PublicAPI]
	[DependsOn<ConfigurationModule>]
	public sealed class DomainModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddDateTimeOffsetProvider",
				services => services.AddDateTimeOffsetProvider());

			context.Log("AddDateTimeProvider",
				services => services.AddDateTimeProvider());
		}
	}
}
