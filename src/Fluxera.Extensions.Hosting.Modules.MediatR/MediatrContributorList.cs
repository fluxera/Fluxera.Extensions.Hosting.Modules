namespace Fluxera.Extensions.Hosting.Modules.MediatR
{
	using System.Collections.Generic;

	/// <summary>
	///     A list that holds the <see cref="IMediatrContributor" /> instances.
	/// </summary>
	internal sealed class MediatrContributorList : List<IMediatrContributor>
	{
	}
}
