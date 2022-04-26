//namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
//{
//	using System;
//	using System.Threading.Tasks;
//	using Fluxera.Guards;
//	using JetBrains.Annotations;

//	[PublicAPI]
//	public static class TenantCacheManager
//	{
//		private static IServiceProvider serviceProvider;

//		public static void SetServiceProvider(Func<IServiceProvider> factory)
//		{
//			Guard.Against.Null(factory, nameof(factory));

//			TenantCacheManager.serviceProvider = factory.Invoke();
//		}

//		public static Task<long> GlobalCachingPrefixCounter()
//		{
//			return Task.FromResult(0L);
//		}

//		public static Task<long> RepositoryCachingPrefixCounter(string repositoryName)
//		{
//			return Task.FromResult(0L);
//		}

//		public static Task<long> TenantCachingPrefixCounter(string repositoryName, string tenant)
//		{
//			return Task.FromResult(0L);
//		}

//		public static Task<long> AggregateCachingPrefixCounter<TAggregateRoot>(string repositoryName, string tenant)
//			where TAggregateRoot : AggregateRoot<TAggregateRoot>
//		{
//			return Task.FromResult(0L);
//		}

//		/// <summary>
//		///     Increment the global counter to invalidate the complete cache for every repository and aggregate.
//		/// </summary>
//		/// <returns></returns>
//		public static Task InvalidateGlobal()
//		{
//			return Task.CompletedTask;
//		}

//		/// <summary>
//		///     Increment the repository counter to invalidate the repository cache for every tenant and aggregate of it.
//		/// </summary>
//		/// <param name="repositoryName"></param>
//		/// <returns></returns>
//		public static Task InvalidateRepository(string repositoryName)
//		{
//			return Task.CompletedTask;
//		}

//		/// <summary>
//		///     Increment the tenant counter to invalidate the tenant cache for every aggregate of a tenant.
//		/// </summary>
//		/// <param name="repositoryName"></param>
//		/// <param name="tenant"></param>
//		/// <returns></returns>
//		public static Task InvalidateTenant(string repositoryName, string tenant)
//		{
//			return Task.CompletedTask;
//		}

//		/// <summary>
//		///     Increment the aggregate counter to invalidate the aggregate cache for a single aggregate type of a tenant.
//		/// </summary>
//		/// <typeparam name="TAggregateRoot"></typeparam>
//		/// <returns></returns>
//		public static Task InvalidateAggregate<TAggregateRoot>(string repositoryName, string tenant)
//			where TAggregateRoot : AggregateRoot<TAggregateRoot>
//		{
//			return Task.CompletedTask;
//		}
//	}
//}


