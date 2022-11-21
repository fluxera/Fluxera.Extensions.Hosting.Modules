namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authentication.ApiKey;

	[UsedImplicitly]
	internal sealed class ApiKeyAuthenticationServiceFactory : IApiKeyAuthenticationServiceFactory
	{
		/// <inheritdoc />
		public IApiKeyAuthenticationService CreateApiKeyAuthenticationService(string authenticationSchemaName)
		{
			throw new NotImplementedException();
		}
	}
}
