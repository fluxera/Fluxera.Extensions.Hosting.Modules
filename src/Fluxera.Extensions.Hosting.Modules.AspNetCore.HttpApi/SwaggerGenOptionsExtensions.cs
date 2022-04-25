namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Microsoft.Extensions.DependencyInjection;
	using Swashbuckle.AspNetCore.SwaggerGen;

	/// <summary>
	///     Extensions methods for the <see cref="SwaggerGenOptions" /> type.
	/// </summary>
	public static class SwaggerGenOptionsExtensions
	{
		/// <summary>
		///     Includes all XML documentation files available.
		/// </summary>
		/// <param name="options"></param>
		public static void IncludeXmlComments(this SwaggerGenOptions options)
		{
			IEnumerable<string> loadedAssemblyNames = AppDomain.CurrentDomain
				.GetAssemblies()
				.Select(x => x.GetName().Name);

			foreach(string assemblyName in loadedAssemblyNames)
			{
				string filePath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
				if(File.Exists(filePath))
				{
					options.IncludeXmlComments(filePath);
				}
			}
		}
	}
}
