﻿namespace Example.Infrastructure
{
	using Fluxera.Extensions.Hosting;
	using Fluxera.Extensions.Hosting.Modules;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Messaging.InMemory;
	using Fluxera.Extensions.Hosting.Modules.Persistence;
	using Fluxera.Extensions.Hosting.Modules.Persistence.InMemory;
	using global::Example.Domain;
	using global::Example.Domain.Example;
	using global::Example.Infrastructure.Contributors;
	using global::Example.Infrastructure.Example;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     The infrastructure module of the component.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(ExampleDomainModule))]
	[DependsOn(typeof(InMemoryMessagingModule))]
	[DependsOn(typeof(InMemoryPersistenceModule))]
	[DependsOn(typeof(ConfigurationModule))]
	public sealed class ExampleInfrastructureModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the repository contributor for the 'Default' repository.
			context.Services.AddRepositoryContributor<RepositoryContributor>("Default");

			// Add repositories.
			context.Log("AddRepositories", services =>
				services.TryAddTransient<IExampleRepository, ExampleRepository>());
		}
	}
}