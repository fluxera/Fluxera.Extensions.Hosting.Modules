namespace Fluxera.Extensions.Hosting.Modules.Messaging.Outbox.EntityFrameworkCore
{
	using System;
	using JetBrains.Annotations;
	using MassTransit;
	using MassTransit.Configuration;
	using Microsoft.EntityFrameworkCore;

	internal static class OutboxConfigurationExtensions
	{
		/// <summary>
		///     Configures the Entity Framework Outbox on the bus, which can subsequently be used to configure
		///     the transactional outbox on a receive endpoint.
		/// </summary>
		/// <param name="configurator"></param>
		/// <param name="configure"></param>
		/// <returns></returns>
		[UsedImplicitly]
		public static void AddEntityFrameworkOutbox<TContext>(this IBusRegistrationConfigurator configurator,
			Action<IEntityFrameworkOutboxConfigurator> configure = null)
			where TContext : DbContext
		{
			EntityFrameworkOutboxConfigurator<TContext> outboxConfigurator = new EntityFrameworkOutboxConfigurator<TContext>(configurator);

			outboxConfigurator.Configure(configure);
		}
	}
}
