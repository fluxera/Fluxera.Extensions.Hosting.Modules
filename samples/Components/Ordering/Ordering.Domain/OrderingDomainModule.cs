namespace Ordering.Domain
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using JetBrains.Annotations;

	/// <summary>
	///     The domain module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn<DomainModule>]
	public sealed class OrderingDomainModule : ConfigureServicesModule
	{
	}
}
