namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using System;
	using global::AspNetCore.Authentication.Basic;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class BasicUserAuthenticationServiceFactory : IBasicUserAuthenticationServiceFactory
	{
		/// <inheritdoc />
		public IBasicUserAuthenticationService CreateBasicUserAuthenticationService(string authenticationSchemaName)
		{
			throw new NotImplementedException();
		}
	}
}
