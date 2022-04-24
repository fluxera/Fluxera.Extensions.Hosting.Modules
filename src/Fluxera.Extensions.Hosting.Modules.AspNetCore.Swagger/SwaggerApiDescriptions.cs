namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Swagger
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     The API description dictionary.
	/// </summary>
	[PublicAPI]
	public sealed class SwaggerApiDescriptions : Dictionary<string, SwaggerApiDescription>
	{
	}
}
