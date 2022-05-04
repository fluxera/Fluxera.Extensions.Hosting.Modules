namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey
{
	using System;
	using global::AspNetCore.Authentication.ApiKey;
	using JetBrains.Annotations;

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
