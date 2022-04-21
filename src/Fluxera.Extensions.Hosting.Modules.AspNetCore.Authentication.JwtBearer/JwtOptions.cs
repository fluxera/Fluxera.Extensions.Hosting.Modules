namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer
{
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class JwtOptions
	{
		public string Authority { get; set; }

		//public string ClientId { get; set; }

		//public string ClientSecret { get; set; }

		public string SigningKey { get; set; }
	}
}
