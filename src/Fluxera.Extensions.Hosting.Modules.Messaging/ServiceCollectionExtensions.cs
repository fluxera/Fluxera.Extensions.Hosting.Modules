namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddConsumersContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IConsumersContributor, new()
		{
			ConsumersContributorList contributorList = services.GetObject<ConsumersContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}

		public static IServiceCollection AddSendEndpointMappingContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, ISendEndpointsContributor, new()
		{
			SendEndpointsContributorList contributorList = services.GetObject<SendEndpointsContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}

		public static IServiceCollection AddTransportContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, ITransportContributor, new()
		{
			TContributor contributor = new TContributor();
			services.TryAddObjectAccessor<ITransportContributor>(contributor);

			return services;
		}
	}
}
