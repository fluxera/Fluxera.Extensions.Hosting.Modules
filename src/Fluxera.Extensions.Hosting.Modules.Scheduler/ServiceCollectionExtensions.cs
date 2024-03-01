namespace Fluxera.Extensions.Hosting.Modules.Scheduler
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
		/// <summary>
		///     Adds the store contributor if no other store contributor was already added.
		/// </summary>
		/// <typeparam name="TContributor"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddStoreContributor<TContributor>(this IServiceCollection services)
			where TContributor : class, IStoreContributor, new()
		{
			TContributor contributor = new TContributor();
			services.TryAddObjectAccessor<IStoreContributor>(contributor);

			return services;
		}
	}
}
