namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds a consumer contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddConsumersContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IConsumersContributor, new()
		{
			ConsumersContributorList contributorList = services.GetObjectOrDefault<ConsumersContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IConsumersContributor));
			}

			return services;
		}

		/// <summary>
		///     Adds a send endpoint mapping contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddSendEndpointMappingContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, ISendEndpointsContributor, new()
		{
			SendEndpointsContributorList contributorList = services.GetObjectOrDefault<SendEndpointsContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(ISendEndpointsContributor));
			}

			return services;
		}

		/// <summary>
		///     Adds the transport contributor if no other transport contributor was already added.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddTransportContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, ITransportContributor, new()
		{
			TContributor contributor = new TContributor();
			services.TryAddObjectAccessor<ITransportContributor>(contributor);

			return services;
		}

		/// <summary>
		///     Adds an outbox contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddOutboxContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IOutboxContributor, new()
		{
			OutboxContributorList contributorList = services.GetObjectOrDefault<OutboxContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IOutboxContributor));
			}

			return services;
		}
	}
}
