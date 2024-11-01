namespace Fluxera.Extensions.Hosting.Modules.Infrastructure.Queries
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries;
	using Fluxera.Queries.Options;
	using JetBrains.Annotations;

	/// <summary>
	///		Extension methods for the <see cref="IEntitySetOptionsBuilder{T}"/> type.
	/// </summary>
	[PublicAPI]
	public static class EntitySetOptionsBuilderExtensions
	{
		///  <summary>
		/// 		Adds the type of the find query to the entity set metadata.
		///  </summary>
		///  <typeparam name="TFindQuery"></typeparam>
		///  <param name="builder"></param>
		///  <returns></returns>
		public static IEntitySetOptionsBuilder UseFind<TFindQuery>(this IEntitySetOptionsBuilder builder)
			where TFindQuery : class, IFindQuery
		{
			return builder.WithMetadata("FindQueryType", typeof(TFindQuery));
		}

		///  <summary>
		/// 		Adds the type of the get query to the entity set metadata.
		///  </summary>
		///  <typeparam name="TGetQuery"></typeparam>
		///  <param name="builder"></param>
		///  <returns></returns>
		public static IEntitySetOptionsBuilder UseGet<TGetQuery>(this IEntitySetOptionsBuilder builder)
			where TGetQuery : class, IGetQuery
		{
			return builder.WithMetadata("GetQueryType", typeof(TGetQuery));
		}

		///  <summary>
		/// 		Adds the type of the count query to the entity set metadata.
		///  </summary>
		///  <typeparam name="TCountQuery"></typeparam>
		///  <param name="builder"></param>
		///  <returns></returns>
		public static IEntitySetOptionsBuilder UseCount<TCountQuery>(this IEntitySetOptionsBuilder builder)
			where TCountQuery : class, ICountQuery
		{
			return builder.WithMetadata("CountQueryType", typeof(TCountQuery));
		}
	}
}
