namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Blazor
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class BlazorAssembliesProvider : IBlazorAssembliesProvider
	{
		private readonly BlazorAssembliesContributorList contributorList;

		public BlazorAssembliesProvider(IObjectAccessor<BlazorAssembliesContributorList> objectAccessor)
		{
			this.contributorList = objectAccessor.Value 
				?? throw new ArgumentException("The assemblies contributor list can't be null.", nameof(contributorList));
		}

		/// <inheritdoc />
		public IEnumerable<Assembly> AdditionalAssemblies => this.contributorList.SelectMany(x => x.AdditionalAssemblies).Distinct();
	}
}
