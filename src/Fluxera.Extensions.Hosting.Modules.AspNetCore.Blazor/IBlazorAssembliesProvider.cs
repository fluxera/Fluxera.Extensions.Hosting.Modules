namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor
{
	using System.Collections.Generic;
	using System.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///		A service that provides access to the configured Blazor assemblies.
	/// </summary>
	[PublicAPI]
	public interface IBlazorAssembliesProvider
	{
		/// <summary>
		///		Gets the app assembly.
		/// </summary>
		Assembly AppAssembly => Assembly.GetEntryAssembly();

		/// <summary>
		///		Gets the additional assemblies.
		/// </summary>
		IEnumerable<Assembly> AdditionalAssemblies { get;}
	}
}
