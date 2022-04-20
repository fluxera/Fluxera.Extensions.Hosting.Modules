namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using Fluxera.Repository;

	internal sealed class AggregateCachingBuilder : IAggregateCachingBuilder
	{
		private readonly IAggregateCachingOptionsBuilder aggregateCachingOptionsBuilder;

		public AggregateCachingBuilder(IAggregateCachingOptionsBuilder aggregateCachingOptionsBuilder)
		{
			this.aggregateCachingOptionsBuilder = aggregateCachingOptionsBuilder;
		}

		/// <inheritdoc />
		public IAggregateCachingBuilder UseNoCachingFor<TAggregateRoot>()
		{
			this.aggregateCachingOptionsBuilder.UseNoCachingFor<TAggregateRoot>();
			return this;
		}

		/// <inheritdoc />
		public IAggregateCachingBuilder UseNoCachingFor(Type type)
		{
			this.aggregateCachingOptionsBuilder.UseNoCachingFor(type);
			return this;
		}

		/// <inheritdoc />
		public IAggregateCachingBuilder UseStandardFor<TAggregateRoot>()
		{
			this.aggregateCachingOptionsBuilder.UseStandardFor<TAggregateRoot>();
			return this;
		}

		/// <inheritdoc />
		public IAggregateCachingBuilder UseStandardFor(Type type)
		{
			this.aggregateCachingOptionsBuilder.UseStandardFor(type);
			return this;
		}

		/// <inheritdoc />
		public IAggregateCachingBuilder UseTimeoutFor<TAggregateRoot>(TimeSpan expiration)
		{
			this.aggregateCachingOptionsBuilder.UseTimeoutFor<TAggregateRoot>(expiration);
			return this;
		}

		/// <inheritdoc />
		public IAggregateCachingBuilder UseTimeoutFor(Type type, TimeSpan expiration)
		{
			this.aggregateCachingOptionsBuilder.UseTimeoutFor(type, expiration);
			return this;
		}
	}
}
