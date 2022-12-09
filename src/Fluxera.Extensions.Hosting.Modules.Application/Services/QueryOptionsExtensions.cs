namespace Fluxera.Extensions.Hosting.Modules.Application.Services
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Query;
	using global::AutoMapper;

	/// <summary>
	///     Extensions methods for the <see cref="IQueryOptions{T}" /> type.
	/// </summary>
	internal static class QueryOptionsExtensions
	{
		/// <summary>
		///     Map the dto-based query options to the entity-based query options.
		/// </summary>
		/// <typeparam name="TDto"></typeparam>
		/// <typeparam name="TAggregateRoot"></typeparam>
		/// <param name="options"></param>
		/// <param name="mapper"></param>
		/// <returns></returns>
		public static Repository.Query.IQueryOptions<TAggregateRoot> MapQueryOptions<TDto, TAggregateRoot>(this IQueryOptions<TDto> options, IMapper mapper)
			where TAggregateRoot : class
			where TDto : class
		{
			Repository.Query.IQueryOptions<TAggregateRoot> mappedOptions = Repository.Query.QueryOptions<TAggregateRoot>.Empty();

			//if(!options.IsEmpty())
			//{
			//	if(options.TryGetSortingOptions(out _))
			//	{
			//		mappedOptions = options.MapSortingOptions<TDto, TAggregateRoot>(mapper);
			//	}
			//	else if(options.TryGetSkipTakeOptions(out _))
			//	{
			//		mappedOptions = options.MapSkipTakeOptions<TDto, TAggregateRoot>();
			//	}
			//	else if(options.TryGetPagingOptions(out _))
			//	{
			//		mappedOptions = options.MapPagingOptions<TDto, TAggregateRoot>();
			//	}
			//}

			//return mappedOptions ?? Repository.Query.QueryOptions<TAggregateRoot>.Empty();

			return mappedOptions;
		}

		private static Repository.Query.IQueryOptions<TAggregateRoot> MapSortingOptions<TDto, TAggregateRoot>(this IQueryOptions<TDto> options, IMapper mapper)
			where TAggregateRoot : class
			where TDto : class
		{
			Repository.Query.IQueryOptions<TAggregateRoot> mappedOptions = null;

			//// Try to map the sorting options of the dto query options.
			//if(options.TryGetSortingOptions(out ISortingOptions<TDto> sortingOptions))
			//{
			//	Expression<Func<TAggregateRoot, object>> mappedOrderByExpression =
			//		mapper.MapExpression<Expression<Func<TAggregateRoot, object>>>(sortingOptions.PrimaryExpression.Expression);
			//	Repository.Query.ISortingOptions<TAggregateRoot> mappedSortingOptions = sortingOptions.PrimaryExpression.IsDescending
			//		? Repository.Query.QueryOptions<TAggregateRoot>.OrderByDescending(mappedOrderByExpression)
			//		: Repository.Query.QueryOptions<TAggregateRoot>.OrderBy(mappedOrderByExpression);

			//	if(sortingOptions.SecondaryExpressions.Any())
			//	{
			//		foreach(ISortExpression<TDto> secondaryExpression in sortingOptions.SecondaryExpressions)
			//		{
			//			Expression<Func<TAggregateRoot, object>> mappedThenByExpression =
			//				mapper.MapExpression<Expression<Func<TAggregateRoot, object>>>(secondaryExpression.Expression);
			//			mappedSortingOptions = secondaryExpression.IsDescending
			//				? mappedSortingOptions.ThenByDescending(mappedThenByExpression)
			//				: mappedSortingOptions.ThenBy(mappedThenByExpression);
			//		}
			//	}

			//	// Try map optional skip/take options.
			//	if(options.TryGetSkipTakeOptions(out ISkipTakeOptions<TDto> skipTakeOptions))
			//	{
			//		int? skipAmount = skipTakeOptions.SkipAmount;
			//		int? takeAmount = skipTakeOptions.TakeAmount;

			//		if(skipAmount.HasValue)
			//		{
			//			Repository.Query.ISkipTakeOptions<TAggregateRoot> mappedSkipTakeOptions = mappedSortingOptions.Skip(skipAmount.Value);

			//			if(takeAmount.HasValue)
			//			{
			//				mappedSkipTakeOptions.Take(takeAmount.Value);
			//			}
			//		}
			//		else
			//		{
			//			if(takeAmount.HasValue)
			//			{
			//				mappedSortingOptions.Take(takeAmount.Value);
			//			}
			//		}
			//	}

			//	// Try map optional paging options.
			//	if(options.TryGetPagingOptions(out IPagingOptions<TDto> pagingOptions))
			//	{
			//		int pageNumberAmount = pagingOptions.PageNumberAmount;
			//		int pageSizeAmount = pagingOptions.PageSizeAmount;

			//		mappedSortingOptions.Paging(pageNumberAmount, pageSizeAmount);
			//	}

			//	mappedOptions = mappedSortingOptions;
			//}

			return mappedOptions;
		}

		private static Repository.Query.IQueryOptions<TAggregateRoot> MapSkipTakeOptions<TDto, TAggregateRoot>(this IQueryOptions<TDto> options)
			where TAggregateRoot : class
			where TDto : class
		{
			Repository.Query.IQueryOptions<TAggregateRoot> mappedOptions = null;

			//// Try to map the skip/take options of the query options.
			//if(options.TryGetSkipTakeOptions(out ISkipTakeOptions<TDto> skipTakeOptions))
			//{
			//	Repository.Query.ISkipTakeOptions<TAggregateRoot> mappedSkipTakeOptions = null;

			//	int? skipAmount = skipTakeOptions.SkipAmount;
			//	int? takeAmount = skipTakeOptions.TakeAmount;

			//	if(skipAmount.HasValue)
			//	{
			//		mappedSkipTakeOptions = Repository.Query.QueryOptions<TAggregateRoot>.Skip(skipAmount.Value);

			//		if(takeAmount.HasValue)
			//		{
			//			mappedSkipTakeOptions = mappedSkipTakeOptions.Take(takeAmount.Value);
			//		}
			//	}
			//	else
			//	{
			//		if(takeAmount.HasValue)
			//		{
			//			mappedSkipTakeOptions = Repository.Query.QueryOptions<TAggregateRoot>.Take(takeAmount.Value);
			//		}
			//	}

			//	mappedOptions = mappedSkipTakeOptions;
			//}

			return mappedOptions;
		}

		private static Repository.Query.IQueryOptions<TAggregateRoot> MapPagingOptions<TDto, TAggregateRoot>(this IQueryOptions<TDto> options)
			where TAggregateRoot : class
			where TDto : class
		{
			Repository.Query.IQueryOptions<TAggregateRoot> mappedOptions = null;

			//// Try to map the paging options of the query options.
			//if(options.TryGetPagingOptions(out IPagingOptions<TDto> pagingOptions))
			//{
			//	int pageNumberAmount = pagingOptions.PageNumberAmount;
			//	int pageSizeAmount = pagingOptions.PageSizeAmount;

			//	Repository.Query.IPagingOptions<TAggregateRoot> mappedPagingOptions =
			//		Repository.Query.QueryOptions<TAggregateRoot>.Paging(pageNumberAmount, pageSizeAmount);
			//	mappedOptions = mappedPagingOptions;
			//}

			return mappedOptions;
		}
	}
}
