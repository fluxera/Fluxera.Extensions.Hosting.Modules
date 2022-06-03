namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Query;
	using JetBrains.Annotations;

	/// <summary>
	///     The contract exposes only "Find" methods as an application service.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	[PublicAPI]
	public interface ICanFindApplicationService<TDto, TKey> : IApplicationService
		where TDto : class, IEntityDto<TKey>
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
		/// <summary>
		///     Find the first item that satisfies the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TDto> FindOneAsync(
			Expression<Func<TDto, bool>> predicate,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default);

		/// <summary>
		///     Find the first item that satisfies the given predicate and result the selected result.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="predicate"></param>
		/// <param name="selector"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<TResult> FindOneAsync<TResult>(
			Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default);

		/// <summary>
		///     Checks if an item exists that satisfies the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<bool> ExistsAsync(
			Expression<Func<TDto, bool>> predicate,
			CancellationToken cancellationToken = default);

		/// <summary>
		///     Find the items that satisfy the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<IReadOnlyCollection<TDto>> FindManyAsync(
			Expression<Func<TDto, bool>> predicate,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default);

		/// <summary>
		///     Find the items that satisfy the given predicate and return the selected results.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="predicate"></param>
		/// <param name="selector"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<IReadOnlyCollection<TResult>> FindManyAsync<TResult>(
			Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector,
			IQueryOptions<TDto> queryOptions = null,
			CancellationToken cancellationToken = default);
	}
}
