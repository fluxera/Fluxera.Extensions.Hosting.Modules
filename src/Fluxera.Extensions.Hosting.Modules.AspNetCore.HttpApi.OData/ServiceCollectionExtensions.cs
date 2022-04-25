namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
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
		public static IServiceCollection AddEdmModelContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IEdmModelContributor, new()
		{
			EdmModelContributorList contributorList = services.GetObject<EdmModelContributorList>();
			TContributor contributor = new TContributor();
			contributorList.Add(contributor);

			return services;
		}

		//public static IServiceCollection AddEdmModelContext(this IServiceCollection services, EdmModelContext context)
		//{
		//	services.TryAddSingleton(context);
		//	return services;
		//}
	}
}
