namespace Fluxera.Extensions.Hosting.Modules.Caching
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Caching.Distributed;

	[PublicAPI]
	public sealed class CachingOptions
	{
		//public CachingOptions()
		//{
		//	//this.GlobalCacheEntryOptions = new DistributedCacheEntryOptions();
		//	this.ConnectionStrings = new ConnectionStrings();
		//}

		public DistributedCacheEntryOptions GlobalCacheEntryOptions { get; set; }

		public CachingConfiguration Caching { get; set; }

		//public ConnectionStrings ConnectionStrings { get; set; }
	}
}
