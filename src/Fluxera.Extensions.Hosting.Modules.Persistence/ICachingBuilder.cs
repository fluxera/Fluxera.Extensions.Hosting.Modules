namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface ICachingBuilder
	{
		IAggregateCachingBuilder UseNoCaching();

		IAggregateCachingBuilder UseStandard();

		IAggregateCachingBuilder UseTimeout(TimeSpan expiration);
	}
}
