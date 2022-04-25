namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     The HTTP API description dictionary.
	/// </summary>
	[PublicAPI]
	public sealed class HttpApiDescriptions : Dictionary<string, HttpApiDescription>
	{
	}
}
