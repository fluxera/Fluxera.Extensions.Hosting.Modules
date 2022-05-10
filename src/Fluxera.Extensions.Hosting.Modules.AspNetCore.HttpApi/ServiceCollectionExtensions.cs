namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using Fluxera.Extensions.DependencyInjection;
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
		///     Add the health check contributor to the list of contributors.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddProblemDetailsContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IProblemDetailsContributor, new()
		{
			ProblemDetailsContributorList contributorList = services.GetObjectOrDefault<ProblemDetailsContributorList>();
			if(contributorList != null)
			{
				TContributor contributor = new TContributor();
				contributorList.Add(contributor);
			}
			else
			{
				ILogger logger = services.GetObjectOrDefault<ILogger>();
				logger?.LogWarning("The contributor list for {Contributor} was not available.", typeof(IProblemDetailsContributor));
			}

			return services;
		}
	}
}
