namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;

	/// <summary>
	///     A static class containing transport header constants.
	/// </summary>
	[PublicAPI]
	public static class TransportHeaders
	{
		/// <summary>
		///     The name of the header containing an access token.
		/// </summary>
		public const string AccessTokenHeaderName = "X-Access-Token";

		/// <summary>
		///     The name of the header containing the application context.
		/// </summary>
		public const string OriginApplicationHeaderName = "X-Origin-Application";
	}
}
