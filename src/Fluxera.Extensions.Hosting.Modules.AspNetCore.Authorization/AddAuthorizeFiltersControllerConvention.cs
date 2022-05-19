namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization
{
	using System.Linq;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc.ApplicationModels;
	using Microsoft.AspNetCore.Mvc.Authorization;

	// https://joonasw.net/view/apply-authz-by-default
	internal sealed class AddAuthorizeFiltersControllerConvention : IControllerModelConvention
	{
		private readonly AuthorizeContributorList authorizeContributorList;
		private readonly IServiceConfigurationContext context;

		public AddAuthorizeFiltersControllerConvention(AuthorizeContributorList authorizeContributorList, IServiceConfigurationContext context)
		{
			this.authorizeContributorList = authorizeContributorList;
			this.context = context;
		}

		public void Apply(ControllerModel controller)
		{
			bool allowAnonymous = this.authorizeContributorList.Any(x => x.AllowAnonymous(controller, this.context));
			if(allowAnonymous)
			{
				return;
			}

			// https://stackoverflow.com/questions/36413476/mvc-core-how-to-force-set-global-authorization-for-all-actions
			// https://stackoverflow.com/a/36415213
			AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.Build();
			controller.Filters.Add(new AuthorizeFilter(policy));
		}
	}
}
