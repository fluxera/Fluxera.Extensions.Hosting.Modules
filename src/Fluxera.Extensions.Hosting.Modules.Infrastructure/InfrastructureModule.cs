namespace Fluxera.Extensions.Hosting.Modules.Infrastructure
{
	using Fluxera.Extensions.Hosting.Modules.Application;
	using JetBrains.Annotations;

	/// <summary>
	///     The infrastructure module.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(ApplicationModule))]
	public sealed class InfrastructureModule : ConfigureServicesModule
	{
	}
}
