namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using Fluxera.Repository;

	internal sealed class EntityCachingBuilder : IEntityCachingBuilder
	{
		private readonly IEntityCachingOptionsBuilder aggregateCachingOptionsBuilder;

		public EntityCachingBuilder(IEntityCachingOptionsBuilder aggregateCachingOptionsBuilder)
		{
			this.aggregateCachingOptionsBuilder = aggregateCachingOptionsBuilder;
		}

		/// <inheritdoc />
		public IEntityCachingBuilder UseNoCachingFor<TAggregateRoot>()
		{
			this.aggregateCachingOptionsBuilder.UseNoCachingFor<TAggregateRoot>();
			return this;
		}

		/// <inheritdoc />
		public IEntityCachingBuilder UseNoCachingFor(Type type)
		{
			this.aggregateCachingOptionsBuilder.UseNoCachingFor(type);
			return this;
		}

		/// <inheritdoc />
		public IEntityCachingBuilder UseStandardFor<TAggregateRoot>()
		{
			this.aggregateCachingOptionsBuilder.UseStandardFor<TAggregateRoot>();
			return this;
		}

		/// <inheritdoc />
		public IEntityCachingBuilder UseStandardFor(Type type)
		{
			this.aggregateCachingOptionsBuilder.UseStandardFor(type);
			return this;
		}

		/// <inheritdoc />
		public IEntityCachingBuilder UseTimeoutFor<TAggregateRoot>(TimeSpan expiration)
		{
			this.aggregateCachingOptionsBuilder.UseTimeoutFor<TAggregateRoot>(expiration);
			return this;
		}

		/// <inheritdoc />
		public IEntityCachingBuilder UseTimeoutFor(Type type, TimeSpan expiration)
		{
			this.aggregateCachingOptionsBuilder.UseTimeoutFor(type, expiration);
			return this;
		}
	}
}
