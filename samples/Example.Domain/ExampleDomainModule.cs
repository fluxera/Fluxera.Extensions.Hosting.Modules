namespace Example.Domain
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using JetBrains.Annotations;

	/// <summary>
	///     The domain module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(DomainModule))]
	[DependsOn(typeof(MessagingModule))]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class ExampleDomainModule : ConfigureServicesModule
	{
	}
}
