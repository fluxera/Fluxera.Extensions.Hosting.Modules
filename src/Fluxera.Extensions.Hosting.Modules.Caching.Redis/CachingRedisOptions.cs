namespace Fluxera.Extensions.Hosting.Modules.Caching.Redis
{
	using Fluxera.Extensions.DataManagement;
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the Redis cache.
	/// </summary>
	[PublicAPI]
	public sealed class CachingRedisOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="CachingRedisOptions" /> type.
		/// </summary>
		public CachingRedisOptions()
		{
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///     Gets or sets the name of the connection string.
		/// </summary>
		public string ConnectionStringName { get; set; } = "Redis";

		/// <summary>
		///     Gets or sets the name of the Redis instance.
		/// </summary>
		public string InstanceName { get; set; } = "RedisDistributedCacheInstance";

		/// <summary>
		///     Gets the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; internal set; }
	}
}
