namespace Fluxera.Extensions.Hosting.Modules.HttpClient
{
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	/// <summary>
	///     The options of the http client module.
	/// </summary>
	[PublicAPI]
	public sealed class HttpClientOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="HttpClientOptions" /> type.
		/// </summary>
		public HttpClientOptions()
		{
			this.RemoteServices = new RemoteServices();
		}

		/// <summary>
		///     Gets the remote services.
		/// </summary>
		public RemoteServices RemoteServices { get; set; }
	}
}
