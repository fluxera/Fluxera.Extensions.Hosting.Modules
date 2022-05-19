namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for an aggregate-specific caching builder.
	/// </summary>
	[PublicAPI]
	public interface IAggregateCachingBuilder
	{
		/// <summary>
		///     Configure not to use caching for the given aggregate root.
		/// </summary>
		/// <typeparam name="TAggregateRoot"></typeparam>
		/// <returns></returns>
		IAggregateCachingBuilder UseNoCachingFor<TAggregateRoot>();

		/// <summary>
		///     Configure not to use caching for the given aggregate root.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IAggregateCachingBuilder UseNoCachingFor(Type type);

		/// <summary>
		///     Configure to use the standard caching strategy for the given aggregate root.
		/// </summary>
		/// <typeparam name="TAggregateRoot"></typeparam>
		/// <returns></returns>
		IAggregateCachingBuilder UseStandardFor<TAggregateRoot>();

		/// <summary>
		///     Configure to use the standard caching strategy for the given aggregate root.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		IAggregateCachingBuilder UseStandardFor(Type type);

		/// <summary>
		///     Configure to use the timeout caching strategy for the given aggregate root.
		/// </summary>
		/// <typeparam name="TAggregateRoot"></typeparam>
		/// <param name="expiration"></param>
		/// <returns></returns>
		IAggregateCachingBuilder UseTimeoutFor<TAggregateRoot>(TimeSpan expiration);

		/// <summary>
		///     Configure to use the timeout caching strategy for the given aggregate root.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="expiration"></param>
		/// <returns></returns>
		IAggregateCachingBuilder UseTimeoutFor(Type type, TimeSpan expiration);
	}
}
