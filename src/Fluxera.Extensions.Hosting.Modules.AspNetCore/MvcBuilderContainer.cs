namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     This container class is used to prevent other modules from accessing
	///     the <see cref="IMvcBuilder" /> directly.
	/// </summary>
	internal sealed class MvcBuilderContainer
	{
		public MvcBuilderContainer(IMvcBuilder builder)
		{
			this.Builder = builder;
		}

		public IMvcBuilder Builder { get; }
	}
}
