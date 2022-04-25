namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Threading.Tasks;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.Abstractions;
	using Microsoft.AspNetCore.Mvc.ActionConstraints;
	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.AspNetCore.Mvc.Routing;
	using Microsoft.Extensions.Logging;

	[UsedImplicitly]
	internal sealed class ControllersEndpointInit : IEndpointInit
	{
		private readonly IActionDescriptorCollectionProvider actionDescriptorCollectionProvider;
		private readonly IHttpClientFactory httpClientFactory;
		private readonly ILogger logger;

		public ControllersEndpointInit(
			ILoggerFactory loggerFactory,
			IHttpClientFactory httpClientFactory,
			IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
		{
			this.logger = loggerFactory.CreateLogger("Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup.EndpointInit");
			this.httpClientFactory = httpClientFactory;
			this.actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
		}

		/// <inheritdoc />
		public async Task InitializeEndpointsAsync()
		{
			foreach(ActionDescriptor actionDescriptor in this.GetGetActionDescriptors())
			{
				AttributeRouteInfo route = actionDescriptor.AttributeRouteInfo;
				IList<ParameterDescriptor> parameters = actionDescriptor.Parameters;

				string relativeUrl = route?.Template ?? string.Empty;

				//if(actionDescriptor.EndpointMetadata.FirstOrDefault(x => x is ApiVersionMetadata) is ApiVersionMetadata versionMetadata)
				//{
				//	ApiVersionModel versionModel = typeof(ApiVersionMetadata)
				//		.GetField("apiModel", BindingFlags.Instance | BindingFlags.NonPublic)?
				//		.GetValue(versionMetadata) as ApiVersionModel;

				//	relativeUrl = relativeUrl.Replace("{version:apiVersion}", versionModel.ToString());
				//}

				foreach(ParameterDescriptor parameter in parameters)
				{
					string dummyValue = GetDummyValueFor(parameter.ParameterType);
					relativeUrl = relativeUrl.Replace($"{{{parameter.Name}}}", dummyValue);
				}

				string url = $"https://localhost:5001/{relativeUrl}";
				this.logger.LogInformation("Initializing endpoint: {Url}", url);

				HttpClient httpClient = this.httpClientFactory.CreateClient();
				await httpClient.GetAsync(url);
			}
		}

		private static string GetDummyValueFor(Type type)
		{
			type = type.UnwrapNullableType();

			string dummyValue;

			if(type.IsNumeric())
			{
				dummyValue = "0";
			}
			else if(type == typeof(Guid))
			{
				dummyValue = Guid.Empty.ToString("D");
			}
			else if(type == typeof(string))
			{
				dummyValue = Guid.Empty.ToString("N");
			}
			else
			{
				dummyValue = Guid.Empty.ToString("N");
			}

			return dummyValue;
		}

		private IEnumerable<ActionDescriptor> GetGetActionDescriptors()
		{
			ActionDescriptorCollection actionDescriptors = this.actionDescriptorCollectionProvider.ActionDescriptors;

			foreach(ActionDescriptor actionDescriptor in actionDescriptors.Items)
			{
				HttpMethodActionConstraint httpMethod = actionDescriptor.ActionConstraints?.FirstOrDefault(x => x is HttpMethodActionConstraint) as HttpMethodActionConstraint;
				if(httpMethod?.HttpMethods.Any(x => x == "GET") == true)
				{
					yield return actionDescriptor;
				}
			}
		}
	}
}
