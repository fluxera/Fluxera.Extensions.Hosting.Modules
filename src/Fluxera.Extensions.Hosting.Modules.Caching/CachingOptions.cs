namespace Fluxera.Extensions.Hosting.Modules.Caching
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Caching.Distributed;
	using Microsoft.Extensions.Caching.Memory;

	/// <summary>
	///     The options for the caching module.
	/// </summary>
	[PublicAPI]
	public sealed class CachingOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="CachingOptions" /> type.
		/// </summary>
		public CachingOptions()
		{
			this.DistributedCache = new DistributedCacheEntryOptions();
		}

		/// <summary>
		///     Gets or sets the memory cache options.
		/// </summary>
		public MemoryCacheEntryOptions MemoryCache { get; set; }

		/// <summary>
		///     Gets or sets the distributed cache options.
		/// </summary>
		public DistributedCacheEntryOptions DistributedCache { get; set; }
	}
}
