namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System.Collections.Generic;

	internal sealed class RepositoryContributorDictionary : Dictionary<string, IList<IRepositoryContributor>>
	{
	}
}
