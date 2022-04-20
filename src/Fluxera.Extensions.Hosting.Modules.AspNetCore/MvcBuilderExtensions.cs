namespace Fluxera.Extensions.Hosting.Modules.AspNetCore
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Extensions.DependencyInjection;
	using Microsoft.AspNetCore.Mvc.ApplicationParts;
	using Microsoft.Extensions.DependencyInjection;

	internal static class MvcBuilderExtensions
	{
		internal static IMvcBuilder AddApplicationParts(this IMvcBuilder builder)
		{
			ApplicationPartManager partManager = builder?.PartManager;
			if(partManager is not null)
			{
				IModuleContainer container = builder.Services.GetSingletonInstance<IModuleContainer>();

				IEnumerable<Assembly> moduleAssemblies = container.Modules.Select(x => x.Assembly).Distinct();

				foreach(Assembly moduleAssembly in moduleAssemblies)
				{
					if(partManager.ApplicationParts.OfType<AssemblyPart>().Any(x => x.Assembly == moduleAssembly))
					{
						continue;
					}

					partManager.ApplicationParts.Add(new AssemblyPart(moduleAssembly));
				}
			}

			return builder;
		}
	}
}
