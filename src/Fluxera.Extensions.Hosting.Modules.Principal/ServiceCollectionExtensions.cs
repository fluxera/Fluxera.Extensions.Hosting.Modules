namespace Fluxera.Extensions.Hosting.Modules.Principal
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Security.Principal;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds a principal provider.
		/// </summary>
		/// <typeparam name="TProvider"></typeparam>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddPrincipalProvider<TProvider>(this IServiceCollection services)
			where TProvider : class, IPrincipalProvider
		{
			services.AddTransient<IPrincipalProvider, TProvider>();

			return services;
		}

		internal static void AddPrincipalAccessor(this IServiceCollection services)
		{
			services.TryAddTransient<IPrincipalAccessor>(serviceProvider =>
			{
				IEnumerable<IPrincipalProvider> contributors = serviceProvider
					.GetServices<IPrincipalProvider>()
					.OrderBy(x => x.Position);
				return new PrincipalAccessor(contributors);
			});

			services.TryAddTransient(provider =>
			{
				IPrincipalAccessor principalAccessor = provider.GetService<IPrincipalAccessor>();
				return principalAccessor?.User as IPrincipal;
			});

			services.TryAddTransient(provider =>
			{
				IPrincipal principal = provider.GetRequiredService<IPrincipal>();
				return principal as ClaimsPrincipal;
			});
		}
	}
}
