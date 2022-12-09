namespace Fluxera.Extensions.Hosting.Modules.ODataClient
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Query;
	using Simple.OData.Client;

	internal static class ODataQueryHelpers
	{
		internal static IBoundClient<TDto> ApplyOptions<TDto>(this IBoundClient<TDto> client, IQueryOptions<TDto> options)
			where TDto : class
		{
			//if(options.TryGetPagingOptions(out IPagingOptions<TDto> pagingOptions))
			//{
			//	client = client.Skip(pagingOptions.SkipAmount).Top(pagingOptions.PageSizeAmount);
			//}

			//if(options.TryGetSkipTakeOptions(out ISkipTakeOptions<TDto> skipTakeOptions))
			//{
			//	if(skipTakeOptions.SkipAmount.HasValue)
			//	{
			//		client = client.Skip(skipTakeOptions.SkipAmount.Value);
			//	}

			//	if(skipTakeOptions.TakeAmount.HasValue)
			//	{
			//		client = client.Top(skipTakeOptions.TakeAmount.Value);
			//	}
			//}

			//if(options.TryGetSortingOptions(out ISortingOptions<TDto> sortingOptions))
			//{
			//	ISortExpression<TDto> primaryExpression = sortingOptions.PrimaryExpression;

			//	IBoundClient<TDto> orderedClient = primaryExpression.IsDescending
			//		? client.OrderByDescending(primaryExpression.Expression)
			//		: client.OrderBy(primaryExpression.Expression);

			//	if(sortingOptions.SecondaryExpressions.Any())
			//	{
			//		foreach(ISortExpression<TDto> secondaryExpression in sortingOptions.SecondaryExpressions)
			//		{
			//			orderedClient = secondaryExpression.IsDescending
			//				? orderedClient.ThenBy(secondaryExpression.Expression)
			//				: orderedClient.ThenByDescending(secondaryExpression.Expression);
			//		}
			//	}

			//	client = orderedClient;
			//}

			return client;
		}
	}
}
