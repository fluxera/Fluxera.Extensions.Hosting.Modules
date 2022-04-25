namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the query operator options.
	/// </summary>
	[PublicAPI]
	public sealed class ODataQueryOperatorOptions
	{
		/// <summary>
		///     Gets or sets a value indicating whether navigation property can be expanded.
		/// </summary>
		public bool EnableExpand { get; set; }

		/// <summary>
		///     Gets or sets a value indicating whether property can be selected.
		/// </summary>
		public bool EnableSelect { get; set; } = true;

		/// <summary>
		///     Gets or sets a value indicating whether entity set and property can apply $count.
		/// </summary>
		public bool EnableCount { get; set; } = true;

		/// <summary>
		///     Gets or sets a value indicating whether property can apply $orderby.
		/// </summary>
		public bool EnableOrderBy { get; set; } = true;

		/// <summary>
		///     Gets or sets a value indicating whether property can apply $filter.
		/// </summary>
		public bool EnableFilter { get; set; } = true;

		/// <summary>
		///     Gets or sets a value indicating whether the service will use skiptoken or not.
		/// </summary>
		public bool EnableSkipToken { get; set; }

		/// <summary>
		///     Gets or sets the max value of $top that a client can request.
		/// </summary>
		/// <value>
		///     The max value of $top that a client can request, or <c>null</c> if there is no limit.
		/// </value>
		public int? MaxTop { get; set; }
	}
}
