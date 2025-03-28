namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using Fluxera.Repository;

	internal sealed class CachingBuilder : ICachingBuilder
	{
		private readonly ICachingOptionsBuilder cachingOptionsBuilder;

		public CachingBuilder(ICachingOptionsBuilder cachingOptionsBuilder)
		{
			this.cachingOptionsBuilder = cachingOptionsBuilder;
		}

		/// <inheritdoc />
		public IEntityCachingBuilder UseNoCaching()
		{
			IEntityCachingOptionsBuilder aggregateCachingOptionsBuilder = this.cachingOptionsBuilder.UseNoCaching();
			return new EntityCachingBuilder(aggregateCachingOptionsBuilder);
		}

		/// <inheritdoc />
		public IEntityCachingBuilder UseStandard()
		{
			IEntityCachingOptionsBuilder aggregateCachingOptionsBuilder = this.cachingOptionsBuilder.UseStandard();
			return new EntityCachingBuilder(aggregateCachingOptionsBuilder);
		}

		/// <inheritdoc />
		public IEntityCachingBuilder UseTimeout(TimeSpan expiration)
		{
			IEntityCachingOptionsBuilder aggregateCachingOptionsBuilder = this.cachingOptionsBuilder.UseTimeout(expiration);
			return new EntityCachingBuilder(aggregateCachingOptionsBuilder);
		}
	}
}
