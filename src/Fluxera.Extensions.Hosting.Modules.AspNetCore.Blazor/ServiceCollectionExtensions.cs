namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Extensions methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Add a blazor assemblies contributor.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddBlazorAssembliesContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IBlazorAssembliesContributor, new()
		{
			BlazorAssembliesContributorList contributorList = services.GetObjectOrDefault<BlazorAssembliesContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger.LogContributorListNotAvailable(typeof(IBlazorAssembliesContributor));
			}

			return services;
		}
	}
}
