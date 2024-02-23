namespace BlazorWasmGlobal.Contributors
{
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor;

	internal sealed class BlazorAssembliesContributor : IBlazorAssembliesContributor
	{
		/// <inheritdoc />
		public IEnumerable<Assembly> AdditionalAssemblies
		{
			get
			{
				yield return typeof(Client._Imports).Assembly;
			}
		}
	}
}
