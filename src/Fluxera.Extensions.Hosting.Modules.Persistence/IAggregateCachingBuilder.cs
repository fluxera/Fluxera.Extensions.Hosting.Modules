namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;

	public interface IAggregateCachingBuilder
	{
		IAggregateCachingBuilder UseNoCachingFor<TAggregateRoot>();

		IAggregateCachingBuilder UseNoCachingFor(Type type);

		IAggregateCachingBuilder UseStandardFor<TAggregateRoot>();

		IAggregateCachingBuilder UseStandardFor(Type type);

		IAggregateCachingBuilder UseTimeoutFor<TAggregateRoot>(TimeSpan expiration);

		IAggregateCachingBuilder UseTimeoutFor(Type type, TimeSpan expiration);
	}
}
