namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication
{
	using Microsoft.AspNetCore.Authentication;

	/// <summary>
	///     This container class is used to prevent other modules from accessing
	///     the <see cref="AuthenticationBuilder" /> directly.
	/// </summary>
	internal sealed class AuthenticationBuilderContainer
	{
		public AuthenticationBuilderContainer(AuthenticationBuilder builder)
		{
			this.Builder = builder;
		}

		public AuthenticationBuilder Builder { get; }
	}
}
