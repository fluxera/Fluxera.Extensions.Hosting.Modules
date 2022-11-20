namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authentication.Basic;

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
