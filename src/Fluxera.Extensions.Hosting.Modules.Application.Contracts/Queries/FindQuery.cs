namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries
{
	using Fluxera.Queries.Options;
	using JetBrains.Annotations;

	/// <summary>
	///		An abstract base class for the find query.
	/// </summary>
	[PublicAPI]
	public abstract class FindQuery : IFindQuery
	{
		/// <summary>
		///		Initialize a new instance of the <see cref="FindQuery"/> type.
		/// </summary>
		/// <param name="queryOptions"></param>
		protected FindQuery(QueryOptions queryOptions)
		{
			this.QueryOptions = queryOptions;
		}

		/// <summary>
		///		Gets the query options.
		/// </summary>
		public QueryOptions QueryOptions { get; }
	}
}
