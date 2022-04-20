namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class AspNetCoreOptions
	{
		public bool UseMvc { get; set; }

		public bool UseViews { get; set; }

		public bool RazorPages { get; set; }
	}
}
