namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries
{
	using Fluxera.Queries.Options;
	using JetBrains.Annotations;

	/// <summary>
	///		An abstract base class for the count query.
	/// </summary>
	[PublicAPI]
	public abstract class CountQuery : ICountQuery
	{
		/// <summary>
		///		Initialize a new instance of the <see cref="CountQuery"/> type.
		/// </summary>
		/// <param name="queryOptions"></param>
		protected CountQuery(QueryOptions queryOptions)
		{
			this.QueryOptions = queryOptions;
		}

		/// <summary>
		///		Gets the query options.
		/// </summary>
		public QueryOptions QueryOptions { get; }
	}
}
