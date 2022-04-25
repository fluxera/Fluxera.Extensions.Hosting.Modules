namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Authorization;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData.Contributors;
	using Fluxera.Extensions.Hosting.Modules.Caching;
	using Fluxera.Extensions.Hosting.Modules.Configuration;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.OData;
	using Microsoft.AspNetCore.OData.Routing.Controllers;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A module that enables OData APIs.
	/// </summary>
	[PublicAPI]
	[DependsOn(typeof(CachingModule))]
	[DependsOn(typeof(AuthorizationModule))]
	[DependsOn(typeof(HttpApiModule))]
	[DependsOn(typeof(AspNetCoreModule))]
	public sealed class ODataModule : ConfigureServicesModule
	{
		/// <inheritdoc />
		public override void PreConfigureServices(IServiceConfigurationContext context)
		{
			// Add the authorize contributor.
			context.Services.AddAuthorizeContributor<AuthorizeContributor>();

			// Add the mvc builder contributor.
			context.Services.AddMvcBuilderContributor<MvcBuilderContributor>();

			// Add the configure options contributor.
			context.Services.AddConfigureOptionsContributor<ConfigureOptionsContributor>();

			// Add the contributor list.
			context.Log("AddObjectAccessor(EdmModelContributorList)",
				services => services.AddObjectAccessor(new EdmModelContributorList(), ObjectAccessorLifetime.ConfigureServices));
		}

		/// <inheritdoc />
		public override void ConfigureServices(IServiceConfigurationContext context)
		{
			context.Services.TryAddTransient<MetadataController>();
			context.Services.AddODataQueryFilter();

			//// Add the idempotent token filter.
			//context.Log("AddIdempotentTokenFilter",
			//	services => services.TryAddScoped<IdempotentTokenFilter>());

			// TODO: Change the base address used by OData to create link refs.
			//context.Services.Configure<MvcOptions>(options =>
			//{
			//	foreach (IOutputFormatter formatter in options.OutputFormatters)
			//	{
			//		if (formatter is ODataOutputFormatter outputFormatter)
			//		{
			//			outputFormatter.BaseAddressFactory = request => new Uri("https://api.fluxera.com/odata");
			//		}
			//	}
			//});
		}
	}
}
