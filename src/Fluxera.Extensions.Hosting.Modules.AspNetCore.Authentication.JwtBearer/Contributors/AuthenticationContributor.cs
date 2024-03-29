﻿namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.JwtBearer.Contributors
{
	using System;
	using System.IdentityModel.Tokens.Jwt;
	using System.Text;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using Fluxera.Utilities.Extensions;
	using IdentityModel;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.AspNetCore.Authentication.JwtBearer;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.IdentityModel.Tokens;

	internal sealed class AuthenticationContributor : IAuthenticationContributor
	{
		/// <inheritdoc />
		public void Configure(AuthenticationBuilder builder, IServiceConfigurationContext context)
		{
			JwtBearerAuthenticationOptions authenticationOptions = context.Services.GetOptions<JwtBearerAuthenticationOptions>();

			// By default, Microsoft has some legacy claim mapping that converts
			// standard JWT claims into proprietary ones. This disables the mappings.
			JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

			// Add all configures JwtBearer schemes.
			foreach((string key, JwtBearerAuthenticationSchemeOptions schemeOptions) in authenticationOptions.Schemes)
			{
				context.Log($"AddJwtBearerAuthentication({key})", _ =>
				{
					string schemeName = key.CalculateSchemeName(JwtBearerDefaults.AuthenticationScheme);

					builder.AddJwtBearer(schemeName, options =>
					{
						if(schemeOptions.Authority.IsNullOrWhiteSpace())
						{
							throw new InvalidOperationException("The Authority configuration value must be set.");
						}

						if(schemeOptions.SigningKey.IsNullOrWhiteSpace())
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
							ClockSkew = TimeSpan.FromSeconds(5),
							IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(schemeOptions.SigningKey))
						};
					});
				});
			}
		}
	}
}
