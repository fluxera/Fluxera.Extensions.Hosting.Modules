namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using System.Collections.Generic;

	/// <summary>
	///     A list that holds the <see cref="IConfigureOptionsContributor" /> instances.
	/// </summary>
	internal sealed class ConfigureContributorList : List<IConfigureOptionsContributor>
	{
	}
}
