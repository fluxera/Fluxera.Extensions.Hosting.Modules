namespace Fluxera.Extensions.Hosting.Modules.Caching
{
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class CachingConfiguration
	{
		public string ConnectionStringName { get; set; } = "Cache";
	}
}
