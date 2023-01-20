namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A tenant options dictionary.
	/// </summary>
	[PublicAPI]
	public sealed class TenantOptionsDictionary : Dictionary<string, TenantOptions>
	{
	}
}
