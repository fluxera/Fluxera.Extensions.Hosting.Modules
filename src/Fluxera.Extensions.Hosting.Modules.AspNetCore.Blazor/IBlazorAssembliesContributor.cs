namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor
{
	using System.Collections.Generic;
	using System.Reflection;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for a Blazor assemblies contributor.
	/// </summary>
	[PublicAPI]
	public interface IBlazorAssembliesContributor
	{
		/// <summary>
		///		Gets the assemblies containing razor pages and components.
		/// </summary>
		IEnumerable<Assembly> AdditionalAssemblies { get; }
	}
}
