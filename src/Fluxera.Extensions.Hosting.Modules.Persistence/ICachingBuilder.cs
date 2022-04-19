namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;

	public interface ICachingBuilder
	{
		IAggregateCachingBuilder UseNoCaching();

		IAggregateCachingBuilder UseStandard();

		IAggregateCachingBuilder UseTimeout(TimeSpan expiration);
	}
}
