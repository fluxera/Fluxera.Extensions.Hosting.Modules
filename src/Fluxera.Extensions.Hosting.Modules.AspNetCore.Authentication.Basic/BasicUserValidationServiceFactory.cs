namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.Basic
{
	using System;
	using global::AspNetCore.Authentication.Basic;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class BasicUserValidationServiceFactory : IBasicUserValidationServiceFactory
	{
		/// <inheritdoc />
		public IBasicUserValidationService CreateBasicUserValidationService(string authenticationSchemaName)
		{
			throw new NotImplementedException();
		}
	}
}
