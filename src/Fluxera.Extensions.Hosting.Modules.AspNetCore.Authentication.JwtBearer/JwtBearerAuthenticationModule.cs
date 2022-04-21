namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer
{
	using System;
	using System.IdentityModel.Tokens.Jwt;
	using System.Text;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Utilities.Extensions;
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
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			context.Log("AddJwtAuthentication", services =>
			{
				JwtBearerAuthenticationOptions authenticationOptions = services.GetOptions<JwtBearerAuthenticationOptions>();

				JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
				services
					.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(options =>
					{
						if(authenticationOptions.Authority.IsNullOrWhiteSpace())
						{
							throw new InvalidOperationException("The Authority configuration value must be set.");
						}

						if(authenticationOptions.SigningKey.IsNullOrWhiteSpace())
						{
							throw new InvalidOperationException("The SigningKey configuration value must be set.");
						}

						options.RequireHttpsMetadata = !context.Environment.IsDevelopment();
						options.SaveToken = true;

						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = false,
							ValidateAudience = false,
							ValidateIssuerSigningKey = true,
							NameClaimType = JwtClaimTypes.Name,
							RoleClaimType = JwtClaimTypes.Role,
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationOptions.SigningKey))
						};
					});
			});
		}
	}
}
