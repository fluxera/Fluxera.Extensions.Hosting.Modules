namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor.UnitTests
{
	using System.Collections.Generic;
	using System.Reflection;

	public class TestBlazorAssembliesContributor : IBlazorAssembliesContributor
	{
		/// <inheritdoc />
		public IEnumerable<Assembly> AdditionalAssemblies
		{
			get
			{
				yield return Assembly.GetExecutingAssembly();
			}
		}
	}
}
