namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer
{
	using System.IdentityModel.Tokens.Jwt;
	using IdentityModel;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.IdentityModel.Tokens;

	/// <summary>
	///     A module that enables JWT Bearer authentication.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AuthenticationModule))]
	public sealed class JwtBearerAuthenticationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddJwtBearer", services =>
			{
				JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
				services
					.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						//JwtOptions jwtOptions = authenticationOptions.Authentication.Jwt;

						//if(jwtOptions.Authority.IsNullOrWhiteSpace())
						//{
						//	throw new InvalidOperationException("The Authentication:Jwt:Authority configuration value must be set.");
						//}

						//if(jwtOptions.SigningKey.IsNullOrWhiteSpace())
						//{
						//	throw new InvalidOperationException("The Authentication:Jwt:SigningKey configuration value must be set.");
						//}

						options.RequireHttpsMetadata = !context.Environment.IsDevelopment();
						options.SaveToken = true;

						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = false,
							ValidateAudience = false,
							ValidateIssuerSigningKey = true,
							NameClaimType = JwtClaimTypes.Name,
							RoleClaimType = JwtClaimTypes.Role,
							//IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.SigningKey))
						};
					});
			});
		}
	}
}
