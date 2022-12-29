// ReSharper disable ReplaceWithSingleCallToSingle

namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.EntityFrameworkCore.Contributors
{
	using System;
	using System.Diagnostics;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Extensions.Hosting.Modules.Persistence.EntityFrameworkCore;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using MassTransit;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     A base class for EFCore transactional outbox configuration.
	/// </summary>
	[UsedImplicitly]
	internal sealed class OutboxContributor : IOutboxContributor
	{
		/// <inheritdoc />
		public void ConfigureOutbox(IBusRegistrationConfigurator configurator, IServiceConfigurationContext context)
		{
			ILogger logger = context.Logger;

			EntityFrameworkCoreMessagingOutboxModuleOptions options = context.Services.GetOptions<EntityFrameworkCoreMessagingOutboxModuleOptions>();

			EntityFrameworkCorePersistenceOptions persistenceOptions = context.Services.GetOptions<EntityFrameworkCorePersistenceOptions>();
			EntityFrameworkCoreRepositoryOptions repositoryOptions = persistenceOptions.Repositories.GetOptionsOrDefault(options.RepositoryName);

			Type dbContextType = Type.GetType(repositoryOptions.DbContextType);
			dbContextType = Guard.Against.Null(dbContextType,
				message: $"The db context must be configured for EFCore repository '{options.RepositoryName}' to be used with the transactional inbox/outbox.");

			MethodInfo methodInfo = typeof(OutboxConfigurationExtensions)
				.GetRuntimeMethods()
				.Where(x => x.Name == "AddEntityFrameworkOutbox")
				.Where(x => x.GetParameters().Length == 2)
				.Where(x => x.GetParameters().First().ParameterType == typeof(IBusRegistrationConfigurator))
				.Single();

			methodInfo
				.MakeGenericMethod(dbContextType)
				.Invoke(null, new object[]
				{
					configurator,
					GetConfigurationAction(options)
				});

			logger.LogEntityFrameworkCoreInboxOutboxUsed();
		}

		private static Action<IEntityFrameworkOutboxConfigurator> GetConfigurationAction(EntityFrameworkCoreMessagingOutboxModuleOptions options)
		{
			void ConfigurationAction(IEntityFrameworkOutboxConfigurator cfg)
			{
				if(options.Outbox.QueryDelay.HasValue)
				{
					cfg.QueryDelay = options.Outbox.QueryDelay.Value;
				}

				if(options.Outbox.QueryTimeout.HasValue)
				{
					cfg.QueryTimeout = options.Outbox.QueryTimeout.Value;
				}

				if(options.Outbox.QueryMessageLimit.HasValue)
				{
					cfg.QueryMessageLimit = options.Outbox.QueryMessageLimit.Value;
				}

				if(options.Outbox.DuplicateDetectionWindow.HasValue)
				{
					cfg.DuplicateDetectionWindow = options.Outbox.DuplicateDetectionWindow.Value;
				}

				// Disable cleanup service if requested.
				if(!options.InboxCleanupServiceEnabled)
				{
					cfg.DisableInboxCleanupService();
				}

				if(options.BusOutbox.UseBusOutbox)
				{
					cfg.UseBusOutbox(config =>
					{
						// Disable delivery service if requested.
						if(!options.DeliveryServiceEnabled)
						{
							config.DisableDeliveryService();
						}

						if(options.BusOutbox.MessageDeliveryLimit.HasValue)
						{
							config.MessageDeliveryLimit = options.BusOutbox.MessageDeliveryLimit.Value;
						}

						if(options.BusOutbox.MessageDeliveryTimeout.HasValue)
						{
							config.MessageDeliveryTimeout = options.BusOutbox.MessageDeliveryTimeout.Value;
						}
					});
				}

				// EFCore specific configuration.
				if(options.Outbox.IsolationLevel.HasValue)
				{
					cfg.IsolationLevel = options.Outbox.IsolationLevel.Value;
				}

				switch(options.Outbox.LockStatementProvider)
				{
					case LockStatementProvider.SqlServer:
						cfg.UseSqlServer();
						break;
					case LockStatementProvider.MySql:
						cfg.UseMySql();
						break;
					case LockStatementProvider.Postgres:
						cfg.UsePostgres();
						break;
					default:
						throw new UnreachableException($"Undefined lock statement provider: '{(int)options.Outbox.LockStatementProvider}'.");
				}
			}

			return ConfigurationAction;
		}
	}
}
