namespace Fluxera.Extensions.Hosting.Modules.ODataClient
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Net;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Query;
	using Fluxera.Extensions.Http;
	using Fluxera.Extensions.OData;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	/// <summary>
	///     A base class for OData client services that only read data.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public abstract class ReadOnlyODataClientServiceBase<T, TKey> : ODataClientServiceBase<T, TKey>
		where T : class, IODataEntity<TKey>
	{
		/// <inheritdoc />
		protected ReadOnlyODataClientServiceBase(string name, string collectionName, IODataClient oDataClient, RemoteService options)
			: base(name, collectionName, oDataClient, options)
		{
		}

		/// <summary>
		///     Gets an items by ID.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<T> GetAsync(TKey id, CancellationToken cancellationToken)
		{
			try
			{
				return await this.ODataClient
					.For<T>(this.CollectionName)
					.Key(id)
					.FindEntryAsync(cancellationToken)
					.ConfigureAwait(false);
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return null;
			}
		}

		/// <summary>
		///     Gets an item by ID and selects a specific value using the given selector.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="id"></param>
		/// <param name="selector"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<TResult> GetAsync<TResult>(TKey id, Expression<Func<T, TResult>> selector, CancellationToken cancellationToken = default)
		{
			try
			{
				T item = await this.ODataClient
					.For<T>(this.CollectionName)
					.Key(id)
					.Select(selector.ConvertSelector())
					.FindEntryAsync(cancellationToken)
					.ConfigureAwait(false);

				// HACK: The OData client should return a dict, object or dynamic instead of the entity.
				return selector.Compile().Invoke(item);
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return default;
			}
		}

		/// <summary>
		///     Checks if an item with the given ID exists.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken)
		{
			T result = await this.GetAsync(id, cancellationToken).ConfigureAwait(false);
			return result != null;
		}

		/// <summary>
		///     Checks if at least one item exists that satisfies the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
		{
			long count = await this.CountAsync(predicate, cancellationToken).ConfigureAwait(false);
			return count > 0;
		}

		/// <summary>
		///     Gets the absolute count of the type.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<long> CountAsync(CancellationToken cancellationToken = default)
		{
			return await this.ODataClient
				.For<T>(this.CollectionName)
				.Count()
				.FindScalarAsync<long>(cancellationToken)
				.ConfigureAwait(false);
		}

		/// <summary>
		///     Gets the count of items that satisfy the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<long> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		{
			return await this.ODataClient
				.For<T>(this.CollectionName)
				.Filter(predicate)
				.Count()
				.FindScalarAsync<long>(cancellationToken)
				.ConfigureAwait(false);
		}

		/// <summary>
		///     Finds the first item that satisfies the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<T> FindOneAsync(Expression<Func<T, bool>> predicate, IQueryOptions<T> queryOptions = null, CancellationToken cancellationToken = default)
		{
			try
			{
				return await this.ODataClient
					.For<T>(this.CollectionName)
					.Filter(predicate)
					.ApplyOptions(queryOptions)
					.FindEntryAsync(cancellationToken)
					.ConfigureAwait(false);
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return null;
			}
		}

		/// <summary>
		///     Finds the first item that satisfies the given predicate and selects a specific value using the given selector.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="predicate"></param>
		/// <param name="selector"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<TResult> FindOneAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, IQueryOptions<T> queryOptions = null, CancellationToken cancellationToken = default)
		{
			try
			{
				T result = await this.ODataClient
					.For<T>(this.CollectionName)
					.Filter(predicate)
					.ApplyOptions(queryOptions)
					.Select(selector.ConvertSelector())
					.FindEntryAsync(cancellationToken)
					.ConfigureAwait(false);

				// HACK: The OData client should return a dict, object or dynamic instead of the entity.
				return selector.Compile().Invoke(result);
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return default;
			}
		}

		/// <summary>
		///     Finds many items that satisfy the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<IReadOnlyCollection<T>> FindManyAsync(Expression<Func<T, bool>> predicate, IQueryOptions<T> queryOptions = null, CancellationToken cancellationToken = default)
		{
			try
			{
				IEnumerable<T> results = await this.ODataClient
					.For<T>(this.CollectionName)
					.Filter(predicate)
					.ApplyOptions(queryOptions)
					.FindEntriesAsync(cancellationToken)
					.ConfigureAwait(false);

				return results.AsReadOnly();
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return Enumerable.Empty<T>().AsReadOnly();
			}
		}

		/// <summary>
		///     Finds many items that satisfy the given predicate and selects a specific value using the given selector.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="predicate"></param>
		/// <param name="selector"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		protected async Task<IReadOnlyCollection<TResult>> FindManyAsync<TResult>(Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, IQueryOptions<T> queryOptions = null, CancellationToken cancellationToken = default)
		{
			try
			{
				IEnumerable<T> results = await this.ODataClient
					.For<T>(this.CollectionName)
					.Filter(predicate)
					.ApplyOptions(queryOptions)
					.Select(selector.ConvertSelector())
					.FindEntriesAsync(cancellationToken)
					.ConfigureAwait(false);

				// HACK: The OData client should return a dict, object or dynamic instead of the entity.
				Func<T, TResult> selectorFunc = selector.Compile();

				IList<TResult> result = new List<TResult>();
				foreach(T item in results)
				{
					result.Add(selectorFunc.Invoke(item));
				}

				return result.AsReadOnly();
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return Enumerable.Empty<TResult>().AsReadOnly();
			}
		}
	}
}
