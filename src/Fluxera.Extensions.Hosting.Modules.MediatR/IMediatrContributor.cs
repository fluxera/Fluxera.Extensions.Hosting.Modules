namespace Fluxera.Extensions.Hosting.Modules.MediatR
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///		A contributor contract for providing additional MediatR configuration.
	/// </summary>
	[PublicAPI]
	public interface IMediatrContributor
	{
		///  <summary>
		/// 	Configure additional MediatR configuration.
		///  </summary>
		///  <param name="context"></param>
		///  <param name="configuration"></param>
		void Configure(IServiceConfigurationContext context, MediatRServiceConfiguration configuration);
	}
}
