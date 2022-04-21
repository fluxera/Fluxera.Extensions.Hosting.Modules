namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contract for a contributor that configures a http-client builder.
	/// </summary>
	[PublicAPI]
	public interface IHttpClientBuilderContributor
	{
		/// <summary>
		///     Configures the given <see cref="IHttpClientBuilder" />.
		/// </summary>
		/// <param name="builder"></param>
		void Configure(IHttpClientBuilder builder);
	}
}
