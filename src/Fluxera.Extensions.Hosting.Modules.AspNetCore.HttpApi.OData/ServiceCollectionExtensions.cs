namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using Asp.Versioning.OData;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Add an edm model contributor.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddEdmModelContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IEdmModelContributor, new()
		{
			TContributor contributor = new TContributor();

			EdmModelContributorList contributorList = services.GetObjectOrDefault<EdmModelContributorList>();
			if(contributorList != null)
			{
				contributorList.Add(contributor);
				services.TryAddSingleton<IModelConfiguration>(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IEdmModelContributor));
			}

			return services;
		}
	}
}
