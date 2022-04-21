namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.ApiKey
{
	using System;
	using System.Threading.Tasks;
	using global::AspNetCore.Authentication.ApiKey;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ApiKeyProvider : IApiKeyProvider
	{
		/// <inheritdoc />
		public async Task<IApiKey> ProvideAsync(string key)
		{
			throw new NotImplementedException();
		}
	}
}
