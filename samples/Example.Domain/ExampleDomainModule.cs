namespace Catalog.Domain
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using JetBrains.Annotations;

	/// <summary>
	///     The domain module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<DomainModule>]
	[DependsOn<ConfigurationModule>]
	public sealed class ExampleDomainModule : ConfigureServicesModule
	{
	}
}
