namespace Fluxera.Extensions.Hosting.Modules.Messaging
{
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ConsumeContextExtensions
	{
		//public static string GetAccessToken(this ConsumeContext context)
		//{
		//	string accessToken = null;

		//	if (context != null)
		//	{
		//		if (context.Headers.TryGetHeader(TransportHeaders.AccessTokenHeaderName, out object result))
		//		{
		//			accessToken = result as string;
		//		}
		//	}

		//	return accessToken;
		//}

		//public static ClaimsPrincipal GetUser(this ConsumeContext context, IPrincipalFactory principalFactory)
		//{
		//	ClaimsPrincipal claimsPrincipal = null;

		//	if (context != null)
		//	{
		//		string accessToken = context.GetAccessToken();
		//		claimsPrincipal = principalFactory.CreatePrincipal(accessToken);
		//	}

		//	return claimsPrincipal;
		//}

		//public static bool IsAuthenticated(this ConsumeContext context, IPrincipalFactory principalFactory)
		//{
		//	bool isAuthenticated = false;

		//	if (context != null)
		//	{
		//		string accessToken = context.GetAccessToken();
		//		ClaimsPrincipal claimsPrincipal = principalFactory.CreatePrincipal(accessToken);
		//		isAuthenticated = claimsPrincipal.Identity.IsAuthenticated;
		//	}

		//	return isAuthenticated;
		//}
	}
}
