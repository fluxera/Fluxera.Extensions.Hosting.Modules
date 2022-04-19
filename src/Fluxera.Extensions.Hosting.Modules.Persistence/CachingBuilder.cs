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
		public IAggregateCachingBuilder UseNoCaching()
		{
			IAggregateCachingOptionsBuilder aggregateCachingOptionsBuilder = this.cachingOptionsBuilder.UseNoCaching();
			return new AggregateCachingBuilder(aggregateCachingOptionsBuilder);
		}

		/// <inheritdoc />
		public IAggregateCachingBuilder UseStandard()
		{
			IAggregateCachingOptionsBuilder aggregateCachingOptionsBuilder = this.cachingOptionsBuilder.UseStandard();
			return new AggregateCachingBuilder(aggregateCachingOptionsBuilder);
		}

		/// <inheritdoc />
		public IAggregateCachingBuilder UseTimeout(TimeSpan expiration)
		{
			IAggregateCachingOptionsBuilder aggregateCachingOptionsBuilder = this.cachingOptionsBuilder.UseTimeout(expiration);
			return new AggregateCachingBuilder(aggregateCachingOptionsBuilder);
		}
	}
}
