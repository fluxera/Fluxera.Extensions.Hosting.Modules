namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using System;
	using System.Security.Claims;
	using System.Text;
	using System.Text.Encodings.Web;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	internal sealed class BasicAuthenticationHandler : AuthenticationHandler<BasicSchemeOptions>
	{
		/// <inheritdoc />
		public BasicAuthenticationHandler(
			IOptionsMonitor<BasicSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder,
			ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
		}

		/// <inheritdoc />
		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			string authHeader = this.Request.Headers["Authorization"].ToString();
			if((authHeader != null) && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
			{
				string token = authHeader.Substring("Basic ".Length).Trim();
				Console.WriteLine(token);
				string credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
				string[] credentials = credentialstring.Split(':');
				if((credentials[0] == "admin") && (credentials[1] == "admin"))
				{
					Claim[] claims = { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, "Admin") };
					ClaimsIdentity identity = new ClaimsIdentity(claims, "Basic");
					ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
					return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, this.Scheme.Name));
				}

				this.Response.StatusCode = 401;
				this.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"dotnetthoughts.net\"");
				return AuthenticateResult.Fail("Invalid Authorization Header");
			}

			this.Response.StatusCode = 401;
			this.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"dotnetthoughts.net\"");
			return AuthenticateResult.Fail("Invalid Authorization Header");
		}
	}
}
