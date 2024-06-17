namespace Fluxera.Extensions.Hosting.Modules.Infrastructure.Queries
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Queries;
	using Fluxera.Queries.Options;
	using JetBrains.Annotations;

	/// <summary>
	///		Extension methods for the <see cref="IEntitySetOptionsBuilder"/> type.
	/// </summary>
	[PublicAPI]
	public static class EntitySetOptionsBuilderExtensions
	{
		/// <summary>
		///		Adds the type of the find query to the entity set metadata.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IEntitySetOptionsBuilder UseFind<T>(this IEntitySetOptionsBuilder builder)
			where T : class, IFindQuery
		{
			return builder.WithMetadata("FindQueryType", typeof(T));
		}

		/// <summary>
		///		Adds the type of the get query to the entity set metadata.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IEntitySetOptionsBuilder UseGet<T>(this IEntitySetOptionsBuilder builder)
			where T : class, IGetQuery
		{
			return builder.WithMetadata("GetQueryType", typeof(T));
		}

		/// <summary>
		///		Adds the type of the count query to the entity set metadata.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IEntitySetOptionsBuilder UseCount<T>(this IEntitySetOptionsBuilder builder)
			where T : class, ICountQuery
		{
			return builder.WithMetadata("CountQueryType", typeof(T));
		}
	}
}
