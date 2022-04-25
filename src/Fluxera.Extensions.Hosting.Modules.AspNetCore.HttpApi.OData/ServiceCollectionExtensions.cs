namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using Asp.Versioning.OData;
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEdmModelContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IEdmModelContributor, new()
		{
			TContributor contributor = new TContributor();

			EdmModelContributorList contributorList = services.GetObject<EdmModelContributorList>();
			contributorList.Add(contributor);
			services.TryAddSingleton<IModelConfiguration>(contributor);

			return services;
		}
	}
}
