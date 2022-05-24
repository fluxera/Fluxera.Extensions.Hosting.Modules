namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services.Query;
	using Microsoft.AspNetCore.OData.Query;
	using Microsoft.OData;

	internal static class ODataQueryOptionsExtensions
	{
		private static readonly MethodInfo QueryableWhere =
			((MethodCallExpression)((Expression<Func<IQueryable<string>>>)
					(() => ((IQueryable<string>)null).Where(x => true)))
				.Body.Unquote())
			.Method.GetGenericMethodDefinition();

		//private static readonly MethodInfo QueryableSelect =
		//	((MethodCallExpression)((Expression<Func<IQueryable<bool>>>)
		//			(() => ((IQueryable<bool>)null).Select(x => true)))
		//		.Body.Unquote())
		//	.Method.GetGenericMethodDefinition();

		private static readonly MethodInfo QueryableOrderBy =
			((MethodCallExpression)((Expression<Func<IQueryable<string>>>)
					(() => ((IQueryable<string>)null).OrderBy(x => true)))
				.Body.Unquote())
			.Method.GetGenericMethodDefinition();

		private static readonly MethodInfo QueryableOrderByDescending =
			((MethodCallExpression)((Expression<Func<IQueryable<string>>>)
					(() => ((IQueryable<string>)null).OrderByDescending(x => true)))
				.Body.Unquote())
			.Method.GetGenericMethodDefinition();

		private static readonly MethodInfo QueryableThenBy =
			((MethodCallExpression)((Expression<Func<IOrderedQueryable<string>>>)
					(() => ((IOrderedQueryable<string>)null).ThenBy(x => true)))
				.Body.Unquote())
			.Method.GetGenericMethodDefinition();

		private static readonly MethodInfo QueryableThenByDescending =
			((MethodCallExpression)((Expression<Func<IOrderedQueryable<string>>>)
					(() => ((IOrderedQueryable<string>)null).ThenByDescending(x => true)))
				.Body.Unquote())
			.Method.GetGenericMethodDefinition();

		internal static Expression<Func<TDto, bool>> ToExpression<TDto>(this FilterQueryOption filter)
			where TDto : class, IEntityDto
		{
			if(filter != null)
			{
				IQueryable<TDto> queryable = Enumerable.Empty<TDto>().AsQueryable();
				queryable = (IQueryable<TDto>)filter.ApplyTo(queryable, new ODataQuerySettings
				{
					HandleNullPropagation = HandleNullPropagationOption.False,
					EnableConstantParameterization = false,
				});

				if(queryable.Expression is MethodCallExpression mce)
				{
					if(!mce.Method.IsGenericMethod || mce.Method.GetGenericMethodDefinition() != QueryableWhere)
					{
						throw new ODataException("Not the desired method call.");
					}

					return mce.Arguments[1].Unquote() as Expression<Func<TDto, bool>>;
				}
			}

			return null;
		}

		internal static ISortingOptions<TDto> ApplyTo<TDto>(this OrderByQueryOption orderBy)
			where TDto : class, IEntityDto
		{
			ISortingOptions<TDto> options = null;

			if(orderBy != null)
			{
				IList<OrderExpressionContainer<TDto>> orderExpressions = orderBy.ToExpressions<TDto>();

				for(int i = 0; i < orderExpressions.Count; i++)
				{
					OrderExpressionContainer<TDto> container = orderExpressions[i];

					// First item is OrderBy | OrderByDescending
					if(i == 0)
					{
						options = container.IsDescending
							? QueryOptions<TDto>.OrderByDescending(container.Expression)
							: QueryOptions<TDto>.OrderBy(container.Expression);
					}
					else
					{
						if(options != null)
						{
							options = container.IsDescending
								? options.ThenByDescending(container.Expression)
								: options.ThenBy(container.Expression);
						}
					}
				}
			}

			return options;
		}

		internal static ISkipTakeOptions<TDto> ApplyTo<TDto>(this SkipQueryOption skip, IQueryOptions<TDto> options)
			where TDto : class, IEntityDto
		{
			ISkipTakeOptions<TDto> skipTakeOptions = null;

			if(skip != null)
			{
				int skipValue = skip.Value;

				skipTakeOptions = options == null
					? QueryOptions<TDto>.Skip(skipValue)
					: ((ISortingOptions<TDto>)options).Skip(skipValue);
			}

			return skipTakeOptions;
		}

		internal static ISkipTakeOptions<TDto> ApplyTo<TDto>(this TopQueryOption top, IQueryOptions<TDto> options)
			where TDto : class, IEntityDto
		{
			ISkipTakeOptions<TDto> result = null;

			if(top != null)
			{
				int topValue = top.Value;

				if(options == null)
				{
					result = QueryOptions<TDto>.Take(topValue);
				}

				if(options is ISortingOptions<TDto> sortingOptions)
				{
					result = sortingOptions.Take(topValue);
				}

				if(options is ISkipTakeOptions<TDto> skipTakeOptions)
				{
					result = skipTakeOptions.Take(topValue);
				}
			}

			return result;
		}

		private static IList<OrderExpressionContainer<TDto>> ToExpressions<TDto>(this OrderByQueryOption orderBy)
			where TDto : class, IEntityDto
		{
			if(orderBy != null)
			{
				IList<OrderExpressionContainer<TDto>> orderByExpressions = new List<OrderExpressionContainer<TDto>>();

				IQueryable<TDto> queryable = Enumerable.Empty<TDto>().AsQueryable();
				queryable = orderBy.ApplyTo(queryable, new ODataQuerySettings
				{
					HandleNullPropagation = HandleNullPropagationOption.False,
					EnableConstantParameterization = false,
				});

				Expression expression = queryable.Expression;
				while(expression != null)
				{
					OrderExpressionContainer<TDto> container = expression.GetExpressionContainer<TDto>(out expression);
					if(container != null)
					{
						orderByExpressions.Add(container);
					}
				}

				return orderByExpressions.Reverse().ToList();
			}

			return null;
		}

		private static OrderExpressionContainer<TDto> GetExpressionContainer<TDto>(this Expression expression, out Expression following)
			where TDto : class, IEntityDto
		{
			following = null;

			if(expression is MethodCallExpression mce)
			{
				if(!mce.Method.IsGenericMethod || !IsOrderMethod(mce.Method.GetGenericMethodDefinition()))
				{
					throw new ODataException("Not the desired method call.");
				}

				bool isDescending = IsDescending(mce.Method.GetGenericMethodDefinition());

				following = mce.Arguments[0].Unquote();
				LambdaExpression lambda = (LambdaExpression)mce.Arguments[1].Unquote();
				MemberExpression member = (MemberExpression)lambda.Body;
				ParameterExpression param = lambda.Parameters[0];

				Expression body = member;
				if(member.Type.IsValueType)
				{
					body = Expression.Convert(member, typeof(object));
				}

				Expression<Func<TDto, object>> orderExpression = Expression.Lambda<Func<TDto, object>>(body, param);
				orderExpression = (Expression<Func<TDto, object>>)orderExpression.Unquote();
				return new OrderExpressionContainer<TDto>
				{
					Expression = orderExpression,
					IsDescending = isDescending,
				};
			}

			return null;
		}

		private static bool IsOrderMethod(MethodInfo methodInfo)
		{
			return methodInfo == QueryableOrderBy ||
				methodInfo == QueryableOrderByDescending ||
				methodInfo == QueryableThenBy ||
				methodInfo == QueryableThenByDescending;
		}

		private static bool IsDescending(MethodInfo methodInfo)
		{
			return methodInfo == QueryableOrderByDescending ||
				methodInfo == QueryableThenByDescending;
		}

		private static Expression Unquote(this Expression quote)
		{
			if(quote.NodeType == ExpressionType.Quote)
			{
				return ((UnaryExpression)quote).Operand.Unquote();
			}

			return quote;
		}
	}
}
