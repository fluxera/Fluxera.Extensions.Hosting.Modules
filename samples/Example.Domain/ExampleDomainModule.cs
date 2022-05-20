namespace Example.Domain
{
	using Example.Domain.ExampleAggregate.Repositories;
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Domain;
	using Fluxera.Extensions.Hosting.Modules.Messaging;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     The domain module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(DomainModule))]
	[DependsOn(typeof(MessagingModule))]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class ExampleDomainModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add repositories.
			context.Log("AddRepositories", services =>
				services.TryAddTransient<IExampleRepository, ExampleRepository>());
		}
	}
}
