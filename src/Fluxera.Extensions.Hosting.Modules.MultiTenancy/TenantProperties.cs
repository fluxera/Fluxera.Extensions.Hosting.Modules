namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///		The tenant's additional properties.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class TenantProperties : Dictionary<string, string>
	{
	}
}
