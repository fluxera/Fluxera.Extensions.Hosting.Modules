namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a caching builder.
	/// </summary>
	[PublicAPI]
	public interface ICachingBuilder
	{
		/// <summary>
		///     Disable caching globally. This configuration can be overriden vor specific aggregates.
		/// </summary>
		/// <returns></returns>
		IEntityCachingBuilder UseNoCaching();

		/// <summary>
		///     Use the standard caching strategy globally. This configuration can be overriden vor specific aggregates.
		/// </summary>
		/// <returns></returns>
		IEntityCachingBuilder UseStandard();

		/// <summary>
		///     Use the timeout caching strategy globally. This configuration can be overriden vor specific aggregates.
		/// </summary>
		/// <param name="expiration"></param>
		/// <returns></returns>
		IEntityCachingBuilder UseTimeout(TimeSpan expiration);
	}
}
