namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.EntityFrameworkCore
{
	using System;
	using System.Data;
	using MassTransit;
	using MassTransit.EntityFrameworkCoreIntegration;
	using MassTransit.Middleware;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	internal sealed class CustomEntityFrameworkOutboxConfigurator<TContext> : IEntityFrameworkOutboxConfigurator
		where TContext : DbContext
	{
		private readonly IBusRegistrationConfigurator configurator;
		private IsolationLevel isolationLevel;
		private ILockStatementProvider lockStatementProvider;

		public CustomEntityFrameworkOutboxConfigurator(IBusRegistrationConfigurator configurator)
		{
			this.configurator = configurator;

			this.lockStatementProvider = new SqlServerLockStatementProvider();
			this.isolationLevel = IsolationLevel.RepeatableRead;
		}

		public TimeSpan DuplicateDetectionWindow { get; set; } = TimeSpan.FromMinutes(30);

		public IsolationLevel IsolationLevel
		{
			set => this.isolationLevel = value;
		}

		public ILockStatementProvider LockStatementProvider
		{
			set => this.lockStatementProvider = value ?? throw new ConfigurationException("LockStatementProvider must not be null");
		}

		public TimeSpan QueryDelay { get; set; } = TimeSpan.FromSeconds(10);

		public int QueryMessageLimit { get; set; } = 100;

		public TimeSpan QueryTimeout { get; set; } = TimeSpan.FromSeconds(30);

		/// <inheritdoc />
		public void DisableInboxCleanupService()
		{
			this.configurator.RemoveHostedService<InboxCleanupService<TContext>>();
		}

		/// <inheritdoc />
		public void UseBusOutbox(Action<IEntityFrameworkBusOutboxConfigurator> configure = null)
		{
			CustomEntityFrameworkBusOutboxConfigurator<TContext> busOutboxConfigurator =
				new CustomEntityFrameworkBusOutboxConfigurator<TContext>(this.configurator, this);

			busOutboxConfigurator.Configure(configure);
		}

		public void Configure(Action<IEntityFrameworkOutboxConfigurator> configure)
		{
			this.configurator.TryAddScoped<IOutboxContextFactory<TContext>, EntityFrameworkOutboxContextFactory<TContext>>();
			this.configurator.AddOptions<EntityFrameworkOutboxOptions>().Configure(options =>
			{
				options.IsolationLevel = this.isolationLevel;
				options.LockStatementProvider = this.lockStatementProvider;
			});

			this.configurator.AddHostedService<InboxCleanupService<TContext>>();
			this.configurator
				.AddOptions<InboxCleanupServiceOptions>()
				.Configure(options =>
				{
					options.DuplicateDetectionWindow = this.DuplicateDetectionWindow;
					options.QueryMessageLimit = this.QueryMessageLimit;
					options.QueryDelay = this.QueryDelay;
					options.QueryTimeout = this.QueryTimeout;
				});

			configure?.Invoke(this);
		}
	}
}
