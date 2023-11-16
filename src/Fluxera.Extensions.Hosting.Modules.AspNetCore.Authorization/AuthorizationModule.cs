namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Principal;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A module that enables authorization.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(AspNetCorePrincipalModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class AuthorizationModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the authorize contributor list.
			context.Log("AddObjectAccessor(AuthorizeContributorList)",
				services => services.AddObjectAccessor(new AuthorizeContributorList(), ObjectAccessorLifetime.ConfigureServices));

			// Add the policies contributor list.
			context.Log("AddObjectAccessor(PolicyContributorList)",
				services => services.AddObjectAccessor(new PolicyContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			// Add the authorization services.
			context.Log("AddAuthorization", services => services.AddAuthorization(options =>
			{
				// Require at least an authenticated used.
				options.FallbackPolicy = new AuthorizationPolicyBuilder()
					.RequireAuthenticatedUser()
					.Build();
			}));
		}

		/// <inheritdoc />
		public override void PostConfigureServices(IServiceConfigurationContext context)
		{
			// Configure options for global authorize attribute.
			AuthorizeContributorList authorizeContributorList = context.Services.GetObject<AuthorizeContributorList>();
			context.Services.Configure<MvcOptions>(options =>
			{
				options.Conventions.Add(new AddAuthorizeFiltersControllerConvention(authorizeContributorList, context));
			});

			// Configure the options for authorization policies.
			PolicyContributorList policyContributorList = context.Services.GetObject<PolicyContributorList>();
			context.Log("AddPolicies", services =>
			{
				services.Configure<AuthorizationOptions>(options =>
				{
					foreach(IPolicyContributor policyContributor in policyContributorList)
					{
						policyContributor.AddPolicy(options, context);
					}
				});
			});

			// Add policy requirements handlers.
			context.Log("AddPolicyHandlers", _ =>
			{
				foreach(IPolicyContributor policyContributor in policyContributorList)
				{
					policyContributor.AddPolicyHandlers(context);
				}
			});
		}
	}
}
