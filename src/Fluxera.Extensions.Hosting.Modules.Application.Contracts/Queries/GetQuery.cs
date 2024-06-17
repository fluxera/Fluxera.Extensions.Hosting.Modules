namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries
{
	using Fluxera.Queries.Options;
	using JetBrains.Annotations;

	/// <summary>
	///		An abstract base class for the get query.
	/// </summary>
	[PublicAPI]
	public abstract class GetQuery<TKey> : IGetQuery
	{
		///  <summary>
		/// 		Initialize a new instance of the <see cref="GetQuery{TKey}"/> type.
		///  </summary>
		///  <param name="id"></param>
		///  <param name="queryOptions"></param>
		protected GetQuery(TKey id, QueryOptions queryOptions)
		{
			this.ID = id;
			this.QueryOptions = queryOptions;
		}

		/// <summary>
		///		Gets the ID for the get query.
		/// </summary>
		public TKey ID { get; }

		/// <summary>
		///		Gets the query options.
		/// </summary>
		public QueryOptions QueryOptions { get; }
	}
}
