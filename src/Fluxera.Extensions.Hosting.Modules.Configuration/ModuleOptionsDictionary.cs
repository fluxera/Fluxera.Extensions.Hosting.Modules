namespace Fluxera.Extensions.Hosting.Modules.Configuration
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A dictionary that holds the app settings for a module.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ModuleOptionsDictionary : Dictionary<string, string>
	{
	}
}
