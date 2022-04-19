namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Extensions.DataManagement;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.DataManagement;
	using Fluxera.Extensions.Hosting.Modules.Persistence.Contributors;
	using Fluxera.Extensions.Validation.DataAnnotations;
	using Fluxera.Extensions.Validation.FluentValidation;
	using Fluxera.Repository;
	using Fluxera.Repository.Caching;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A module that enabled persistence.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(DataManagementModule))]
	public sealed class PersistenceModule : ConfigureApplicationModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Initialize the assembly contributor list.
			context.Log("AddObjectAccessor(RepositoryContributorDictionary)",
				services => services.AddObjectAccessor(new RepositoryContributorDictionary(), ObjectAccessorLifetime.ConfigureServices));

			// Initialize the repository provider contributor list.
			context.Log("AddObjectAccessor(RepositoryProviderContributorList)",
				services => services.AddObjectAccessor(new RepositoryProviderContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// Add database name provider.
			context.Log("AddDatabaseNameProvider", services =>
			{
				services.TryAddTransient<IDatabaseNameProviderAdapter, DefaultDatabaseNameProviderAdapter>();
				services.ReplaceTransient<IDatabaseNameProvider, DatabaseNameProvider>();
			});

			// Get the assembly contributors.
			RepositoryContributorDictionary contributorDictionary = context.Services.GetObject<RepositoryContributorDictionary>();

			// Get the repository provider contributors.
			RepositoryProviderContributorList contributorList = context.Services.GetObject<RepositoryProviderContributorList>();

			// Get persistence options to use in service configuration.
			PersistenceOptions persistenceOptions = context.Services.GetOptions<PersistenceOptions>();
			persistenceOptions.ConnectionStrings = context.Services.GetObject<ConnectionStrings>();

			// Add default services and the repositories.
			context.Services.AddRepository(builder =>
			{
				foreach((string repositoryName, RepositoryOptions repositoryOptions) in persistenceOptions.Repositories)
				{
					IList<IRepositoryContributor> repositoryContributors = contributorDictionary.GetOrDefault(repositoryName);
					if(repositoryContributors == null)
					{
						throw new InvalidOperationException($"No repository contributor was added for repository '{repositoryName}'.");
					}

					context.Log($"AddRepository({repositoryName}, {repositoryOptions.ProviderName})", s =>
					{
						if(string.IsNullOrWhiteSpace(repositoryOptions.ProviderName))
						{
							throw new InvalidOperationException("No repository provider specified.");
						}

						// Get the connection string for the repository.
						string connectionString = persistenceOptions.ConnectionStrings[repositoryOptions.ConnectionStringName];

						// Select a configured repository provider.
						IRepositoryProviderContributor repositoryProviderContributor = contributorList.FirstOrDefault(x => x.RepositoryProviderName == repositoryOptions.ProviderName);
						if(repositoryProviderContributor == null)
						{
							throw new InvalidOperationException($"No repository provider contributor found for provider '{repositoryOptions.ProviderName}'.");
						}

						// Configure the used provider.
						repositoryProviderContributor.AddRepository(builder, repositoryName, repositoryOptionsBuilder =>
						{
							// Configure for what aggregate root types this repository uses.
							foreach(IRepositoryContributor persistenceContributor in repositoryContributors)
							{
								IRepositoryAggregatesBuilder repositoryAggregatesBuilder = new RepositoryAggregatesBuilder(repositoryOptionsBuilder);
								persistenceContributor.ConfigureAggregates(repositoryAggregatesBuilder);
							}

							repositoryProviderContributor.ConfigureRepository(repositoryOptionsBuilder, connectionString, repositoryOptions);

							// Configure the domain event handlers.
							repositoryOptionsBuilder.AddDomainEventHandling(domainHandlerOptionsBuilder =>
							{
								foreach(IRepositoryContributor repositoryContributor in repositoryContributors)
								{
									IEventHandlersBuilder eventHandlersBuilder = new EventHandlersBuilder(domainHandlerOptionsBuilder);
									repositoryContributor.ConfigureEventHandling(eventHandlersBuilder);
								}
							});

							// Configure validation providers.
							repositoryOptionsBuilder.AddValidation(validationOptionsBuilder =>
							{
								validationOptionsBuilder.AddValidatorFactory(validationBuilder =>
								{
									validationBuilder.AddDataAnnotations(validationOptionsBuilder.RepositoryName);
									validationBuilder.AddFluentValidation(validationOptionsBuilder.RepositoryName, fluentValidationOptions =>
									{
										foreach(IRepositoryContributor repositoryContributor in repositoryContributors)
										{
											IValidatorBuilder validatorBuilder = new ValidatorBuilder(fluentValidationOptions);
											repositoryContributor.ConfigureValidation(validatorBuilder);
										}
									});
								});
							});

							// Configure the interception.
							repositoryOptionsBuilder.AddInterception(interceptionOptionsBuilder =>
							{
								foreach(IRepositoryContributor repositoryContributor in repositoryContributors)
								{
									IInterceptionBuilder interceptionBuilder = new InterceptionBuilder(interceptionOptionsBuilder);
									repositoryContributor.ConfigureInterception(interceptionBuilder);
								}
							});

							// Configure caching.
							repositoryOptionsBuilder.AddCaching(cachingOptionsBuilder =>
							{
								foreach(IRepositoryContributor repositoryContributor in repositoryContributors)
								{
									ICachingBuilder cachingBuilder = new CachingBuilder(cachingOptionsBuilder);
									repositoryContributor.ConfigureCaching(cachingBuilder);
								}
							});
						});
					});
				}
			});
		}

		/// <inheritdoc />
		public override void PostConfigure(IApplicationInitializationContext context)
		{
			context.Log("Initialize(CachePrefixManager)", serviceProvider =>
			{
				ICachePrefixManager cachePrefixManager = serviceProvider.GetRequiredService<ICachePrefixManager>();
				CacheManager.CachePrefixManager = cachePrefixManager;
			});
		}
	}
}
