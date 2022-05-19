namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using System.Security.Claims;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///     Extension methods for the <see cref="ConsumeContext" /> type.
	/// </summary>
	[PublicAPI]
	public static class ConsumeContextExtensions
	{
		/// <summary>
		///     Gets the access-token from the consumer context headers.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static string GetAccessToken(this ConsumeContext context)
		{
			string accessToken = null;

			if(context != null && context.Headers.TryGetHeader(TransportHeaders.AccessTokenHeaderName, out object result))
			{
				accessToken = result as string;
			}

			return accessToken;
		}

		/// <summary>
		///     Gets the user from the consumer context.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="principalFactory"></param>
		/// <returns></returns>
		public static ClaimsPrincipal GetUser(this ConsumeContext context, IPrincipalFactory principalFactory)
		{
			ClaimsPrincipal claimsPrincipal = null;

			if(context != null)
			{
				string accessToken = context.GetAccessToken();
				claimsPrincipal = principalFactory.CreatePrincipal(accessToken);
			}

			return claimsPrincipal;
		}

		/// <summary>
		///     Checks if the user from the consumer context is authenticated..
		/// </summary>
		/// <param name="context"></param>
		/// <param name="principalFactory"></param>
		/// <returns></returns>
		public static bool IsAuthenticated(this ConsumeContext context, IPrincipalFactory principalFactory)
		{
			bool isAuthenticated = false;

			if(context != null)
			{
				string accessToken = context.GetAccessToken();
				ClaimsPrincipal claimsPrincipal = principalFactory.CreatePrincipal(accessToken);
				isAuthenticated = claimsPrincipal?.Identity?.IsAuthenticated ?? false;
			}

			return isAuthenticated;
		}
	}
}
