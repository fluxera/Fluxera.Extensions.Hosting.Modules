namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System;
	using System.IdentityModel.Tokens.Jwt;
	using System.Security.Claims;
	using System.Text;
	using IdentityModel;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using Microsoft.IdentityModel.Tokens;

	[UsedImplicitly]
	internal sealed class PrincipalFactory : IPrincipalFactory
	{
		private readonly ILogger<PrincipalFactory> logger;
		private readonly MessagingOptions options;

		public PrincipalFactory(
			ILogger<PrincipalFactory> logger,
			IOptions<MessagingOptions> options)
		{
			this.logger = logger;
			this.options = options.Value;
		}

		/// <inheritdoc />
		public ClaimsPrincipal CreatePrincipal(string accessToken)
		{
			ClaimsPrincipal principal = null;

			try
			{
				JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
				handler.InboundClaimTypeMap.Clear();

				TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = false,
					ValidateIssuer = false,
					ValidateIssuerSigningKey = true,
					ValidateActor = false,
					ValidateLifetime = false,
					ValidateTokenReplay = false,
					NameClaimType = JwtClaimTypes.Name,
					RoleClaimType = JwtClaimTypes.Role,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.SigningKey))
				};

				principal = handler.ValidateToken(accessToken, tokenValidationParameters, out _);
			}
			catch(Exception ex)
			{
				logger.LogError(ex, ex.Message);
			}

			return principal ?? new ClaimsPrincipal(new ClaimsIdentity());
		}
	}
}
