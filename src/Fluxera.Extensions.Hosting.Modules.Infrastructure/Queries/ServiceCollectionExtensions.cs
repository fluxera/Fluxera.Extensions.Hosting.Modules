namespace Fluxera.Extensions.Hosting.Modules.Infrastructure.Queries
{
	using Fluxera.Queries;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///		Extensions methods for the <see cref="IServiceCollection"/> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///		Adds the delegating query executor services.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDelegatingQueryExecutor(this IServiceCollection services)
		{
			services.TryAddTransient(typeof(IQueryExecutor<,>), typeof(DelegatingQueryExecutor<,>));

			return services;
		}
	}
}
