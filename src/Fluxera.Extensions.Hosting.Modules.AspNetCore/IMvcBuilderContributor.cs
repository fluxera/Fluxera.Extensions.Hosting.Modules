namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contract for contributors that utilize the <see cref="IMvcBuilder" /> instance.
	/// </summary>
	[PublicAPI]
	public interface IMvcBuilderContributor
	{
		/// <summary>
		///     Configure the <see cref="IMvcBuilder" />.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		void Configure(IMvcBuilder builder, IServiceConfigurationContext context);
	}
}
