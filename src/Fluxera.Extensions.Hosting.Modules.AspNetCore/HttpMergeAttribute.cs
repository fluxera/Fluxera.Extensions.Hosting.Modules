// ReSharper disable once CheckNamespace

namespace Microsoft.AspNetCore.Mvc
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc.Routing;

	/// <summary>
	///     Identifies an action that supports the HTTP MERGE method.
	/// </summary>
	[PublicAPI]
	public class HttpMergeAttribute : HttpMethodAttribute
	{
		private static readonly IEnumerable<string> SupportedMethods = new string[] { "MERGE" };

		/// <summary>
		///     Creates a new instance of the <see cref="HttpMergeAttribute" /> type.
		/// </summary>
		public HttpMergeAttribute() : base(SupportedMethods)
		{
		}

		/// <summary>
		///     Creates a new instance of the <see cref="HttpMergeAttribute" /> type with the given route template.
		/// </summary>
		/// <param name="template">The route template. May not be null.</param>
		public HttpMergeAttribute(string template) : base(SupportedMethods, template)
		{
			if(template == null)
			{
				throw new ArgumentNullException(nameof(template));
			}
		}
	}
}
