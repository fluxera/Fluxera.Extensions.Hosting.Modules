namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup
{
	using System;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Abstractions;
	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.AspNetCore.Routing;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	[UsedImplicitly]
	internal sealed class ActionsEndpointInit : IEndpointInit
	{
		private readonly IActionDescriptorCollectionProvider actionDescriptorCollectionProvider;
		private readonly IActionInvokerFactory invokerFactory;
		private readonly ILogger logger;
		private readonly IServiceProvider serviceProvider;

		public ActionsEndpointInit(
			ILoggerFactory loggerFactory,
			IServiceProvider serviceProvider,
			IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
			IActionInvokerFactory invokerFactory)
		{
			this.logger = loggerFactory.CreateLogger("Fluxera.Extensions.Hosting.Modules.AspNetCore.Warmup.EndpointInit");
			this.serviceProvider = serviceProvider;
			this.actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
			this.invokerFactory = invokerFactory;
		}

		/// <inheritdoc />
		public Task InitializeAsync()
		{
			this.logger.LogInitializingEndpoints();

			ActionDescriptorCollection actions = this.actionDescriptorCollectionProvider.ActionDescriptors;

			using(IServiceScope serviceScope = this.serviceProvider.CreateScope())
			{
				HttpContext httpContext = new DefaultHttpContext
				{
					RequestServices = serviceScope.ServiceProvider
				};

				foreach(ActionDescriptor actionDescriptor in actions.Items)
				{
					this.logger.LogInitializingEndpoint(
						actionDescriptor.RouteValues["controller"],
						actionDescriptor.RouteValues["action"]);

					RouteData routeData = new RouteData();
					foreach((string key, string value) in actionDescriptor.RouteValues)
					{
						routeData.Values.Add(key, value);
					}

					ActionDescriptor descriptor = actionDescriptor;

					if(descriptor is PageActionDescriptor pageActionDescriptor)
					{
						descriptor = pageActionDescriptor;
					}

					ActionContext context = new ActionContext(httpContext, routeData, descriptor);
					IActionInvoker _ = this.invokerFactory.CreateInvoker(context);
				}
			}

			return Task.CompletedTask;
		}
	}
}
