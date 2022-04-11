namespace Fluxera.Extensions.Hosting.Modules.ODataClient
{
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	/// <summary>
	///     The OData client module options.
	/// </summary>
	[PublicAPI]
	public sealed class ODataClientOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ODataClientOptions" /> type.
		/// </summary>
		public ODataClientOptions()
		{
			this.RemoteServices = new RemoteServices();
		}

		/// <summary>
		///     Gets the remote services.
		/// </summary>
		public RemoteServices RemoteServices { get; set; }
	}
}
