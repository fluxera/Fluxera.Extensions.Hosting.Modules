namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey
{
	using System;
	using global::AspNetCore.Authentication.ApiKey;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ApiKeyProviderFactory : IApiKeyProviderFactory
	{
		/// <inheritdoc />
		public IApiKeyProvider CreateApiKeyProvider(string authenticationSchemaName)
		{
			throw new NotImplementedException();
		}
	}
}
