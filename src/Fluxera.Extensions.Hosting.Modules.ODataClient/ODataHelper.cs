namespace Fluxera.Extensions.Hosting.Modules.ODataClient
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Net;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using Fluxera.Extensions.OData;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	/// <summary>
	///     A helper class with default implementations for accessing an OData feed.
	/// </summary>
	/// <typeparam name="TDto"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public sealed class ODataHelper<TDto, TKey>
		where TDto : class, IEntityDto<TKey>
		where TKey : IComparable<TKey>, IEquatable<TKey>
	{
		private readonly IODataClient oDataClient;
		private readonly string collectionName;

		/// <summary>
		///     Initializes a new instance of the <see cref="ODataHelper{TDto, TKey}" /> type.
		/// </summary>
		/// <param name="oDataClient"></param>
		/// <param name="collectionName"></param>
		public ODataHelper(IODataClient oDataClient, string collectionName)
		{
			this.oDataClient = oDataClient;
			this.collectionName = collectionName;
		}

		/// <summary>
		///     Adds the given item.
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task AddAsync(TDto dto, CancellationToken cancellationToken)
		{
			dto = Guard.Against.Null(dto);

			if(!dto.IsTransient())
			{
				throw Errors.CanNotAddExistingItem();
			}

			TDto result = await this.oDataClient
				.For<TDto>(this.collectionName)
				.Set(dto)
				.InsertEntryAsync(cancellationToken)
				.ConfigureAwait(false);

			dto.ID = result.ID;
			this.TransferAuditValues(result, dto);
		}

		/// <summary>
		///     Adds the given items.
		/// </summary>
		/// <param name="dtos"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task AddRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			dtos = Guard.Against.Null(dtos).ToList();

			if(dtos.Any(x => !x.IsTransient()))
			{
				throw Errors.CanNotAddExistingItem();
			}

			ODataBatch batch = new ODataBatch(this.oDataClient);

			foreach(TDto dto in dtos)
			{
				batch += async client =>
				{
					TDto result = await client
						.For<TDto>(this.collectionName)
						.Set(dto)
						.InsertEntryAsync(cancellationToken)
						.ConfigureAwait(false);

					dto.ID = result.ID;
					this.TransferAuditValues(result, dto);
				};
			}

			await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Updates the given item.
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task UpdateAsync(TDto dto, CancellationToken cancellationToken)
		{
			dto = Guard.Against.Null(dto);

			if(dto.IsTransient())
			{
				throw Errors.CanNotUpdateTransientItem();
			}

			object data = dto;
			// ReSharper disable once SuspiciousTypeConversion.Global
			if(dto is IPatchableObject patchableObject)
			{
				data = patchableObject.ChangeTracker.GetChangesObject();
			}

			TDto result = await this.oDataClient
				.For<TDto>(this.collectionName)
				.Key(dto.ID)
				.Set(data)
				.UpdateEntryAsync(cancellationToken)
				.ConfigureAwait(false);

			this.TransferAuditValues(result, dto);
		}

		/// <summary>
		///     Updates the given items.
		/// </summary>
		/// <param name="dtos"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task UpdateRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			dtos = Guard.Against.Null(dtos).ToList();

			if(dtos.Any(x => x.IsTransient()))
			{
				throw Errors.CanNotUpdateTransientItem();
			}

			ODataBatch batch = new ODataBatch(this.oDataClient);

			foreach(TDto dto in dtos)
			{
				object data = dto;
				// ReSharper disable once SuspiciousTypeConversion.Global
				if(dto is IPatchableObject patchableObject)
				{
					data = patchableObject.ChangeTracker.GetChangesObject();
				}

				batch += async client =>
				{
					TDto result = await client
						.For<TDto>(this.collectionName)
						.Key(dto.ID)
						.Set(data)
						.UpdateEntryAsync(cancellationToken)
						.ConfigureAwait(false);

					this.TransferAuditValues(result, dto);
				};
			}

			await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Deletes the given item.
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task DeleteAsync(TDto dto, CancellationToken cancellationToken = default)
		{
			dto = Guard.Against.Null(dto);

			if(dto.IsTransient())
			{
				throw Errors.CanNotDeleteTransientItem();
			}

			await this.oDataClient
				.For<TDto>(this.collectionName)
				.Key(dto.ID)
				.DeleteEntryAsync(cancellationToken)
				.ConfigureAwait(false);
		}

		/// <summary>
		///     Deletes an item by ID.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task DeleteAsync(string id, CancellationToken cancellationToken)
		{
			id = Guard.Against.NullOrWhiteSpace(id);

			await this.oDataClient
				.For<TDto>(this.collectionName)
				.Key(id)
				.DeleteEntryAsync(cancellationToken)
				.ConfigureAwait(false);
		}

		/// <summary>
		///     Deletes the given items.
		/// </summary>
		/// <param name="dtos"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task DeleteRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		{
			dtos = Guard.Against.Null(dtos).ToList();

			if(dtos.Any(x => x.IsTransient()))
			{
				throw Errors.CanNotDeleteTransientItem();
			}

			ODataBatch batch = new ODataBatch(this.oDataClient);

			foreach(TDto dto in dtos)
			{
				batch += async client => await client
					.For<TDto>(this.collectionName)
					.Key(dto.ID)
					.DeleteEntryAsync(cancellationToken)
					.ConfigureAwait(false);
			}

			await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Deletes the items that satisfy the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task DeleteRangeAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			predicate = Guard.Against.Null(predicate);

			await this.oDataClient
				.For<TDto>(this.collectionName)
				.Filter(predicate)
				.DeleteEntryAsync(cancellationToken)
				.ConfigureAwait(false);
		}

		/// <summary>
		///     Gets an item by ID.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TDto> GetAsync(string id, CancellationToken cancellationToken)
		{
			try
			{
				return await this.oDataClient
					.For<TDto>(this.collectionName)
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
		///     Gets an item by ID and returns the values selected by the selector.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="id"></param>
		/// <param name="selector"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TResult> GetAsync<TResult>(string id, Expression<Func<TDto, TResult>> selector, CancellationToken cancellationToken = default)
		{
			try
			{
				TDto item = await this.oDataClient
					.For<TDto>(this.collectionName)
					.Key(id)
					.Select(selector.ConvertSelector())
					.FindEntryAsync(cancellationToken)
					.ConfigureAwait(false);

				// HACK: The OData client should return a dict, object or dynamic instead of the entity.
				// ReSharper disable once ConditionIsAlwaysTrueOrFalse
				return item != null ? selector.Compile().Invoke(item) : default;
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return default;
			}
		}

		/// <summary>
		///     Checks if the item with the given ID exists.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken)
		{
			TDto result = await this.GetAsync(id, cancellationToken).ConfigureAwait(false);
			return result != null;
		}

		/// <summary>
		///     Checks if items satisfying by the given predicate exist.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<bool> ExistsAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			long count = await this.CountAsync(predicate, cancellationToken).ConfigureAwait(false);
			return count > 0;
		}

		/// <summary>
		///     Gets the total count of items.
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<long> CountAsync(CancellationToken cancellationToken = default)
		{
			return await this.oDataClient
				.For<TDto>(this.collectionName)
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
		public async Task<long> CountAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken)
		{
			return await this.oDataClient
				.For<TDto>(this.collectionName)
				.Filter(predicate)
				.Count()
				.FindScalarAsync<long>(cancellationToken)
				.ConfigureAwait(false);
		}

		/// <summary>
		///		Find an item that matches the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TDto> FindOneAsync(
			Expression<Func<TDto, bool>> predicate,
			Func<IBoundClient<TDto>, IBoundClient<TDto>> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			try
			{
				return await this.oDataClient
					.For<TDto>(this.collectionName)
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
		///		Find an item that matches the given predicate and return the selected value.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="predicate"></param>
		/// <param name="selector"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TResult> FindOneAsync<TResult>(
			Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector,
			Func<IBoundClient<TDto>, IBoundClient<TDto>> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			try
			{
				TDto item = await this.oDataClient
					.For<TDto>(this.collectionName)
					.Filter(predicate)
					.ApplyOptions(queryOptions)
					.Select(selector.ConvertSelector())
					.FindEntryAsync(cancellationToken)
					.ConfigureAwait(false);

				// HACK: The OData client should return a dict, object or dynamic instead of the entity.
				// ReSharper disable once ConditionIsAlwaysTrueOrFalse
				return item != null ? selector.Compile().Invoke(item) : default;
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return default;
			}
		}

		/// <summary>
		///		Find items that match the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IReadOnlyCollection<TDto>> FindManyAsync(
			Expression<Func<TDto, bool>> predicate,
			Func<IBoundClient<TDto>, IBoundClient<TDto>> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			try
			{
				IEnumerable<TDto> results = await this.oDataClient
					.For<TDto>(this.collectionName)
					.Filter(predicate)
					.ApplyOptions(queryOptions)
					.FindEntriesAsync(cancellationToken)
					.ConfigureAwait(false);

				return results.AsReadOnly();
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return Enumerable.Empty<TDto>().AsReadOnly();
			}
		}

		/// <summary>
		///		Find items that match the given predicate and return the selected value.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="predicate"></param>
		/// <param name="selector"></param>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IReadOnlyCollection<TResult>> FindManyAsync<TResult>(
			Expression<Func<TDto, bool>> predicate,
			Expression<Func<TDto, TResult>> selector,
			Func<IBoundClient<TDto>, IBoundClient<TDto>> queryOptions = null,
			CancellationToken cancellationToken = default)
		{
			try
			{
				IEnumerable<TDto> results = await this.oDataClient
					.For<TDto>(this.collectionName)
					.Filter(predicate)
					.ApplyOptions(queryOptions)
					.Select(selector.ConvertSelector())
					.FindEntriesAsync(cancellationToken)
					.ConfigureAwait(false);

				// HACK: The OData client should return a dict, object or dynamic instead of the entity.
				Func<TDto, TResult> selectorFunc = selector.Compile();
				return results.Select(item => item != null ? selectorFunc.Invoke(item) : default).ToList().AsReadOnly();
			}
			catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
			{
				return Enumerable.Empty<TResult>().AsReadOnly();
			}
		}

		// TODO: Aggregate methods

		/// <summary>
		///		Executes a scalar result function.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="functionName"></param>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TResult> ExecuteFunctionScalarAsync<TResult>(string functionName, object parameters = null,
			CancellationToken cancellationToken = default)
			where TResult : struct, IConvertible //, INumber<TResult>
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Function(functionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Executes a single item result function.
		/// </summary>
		/// <param name="functionName"></param>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TDto> ExecuteFunctionSingleAsync(string functionName, object parameters = null, CancellationToken cancellationToken = default)
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Function(functionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Executes a multiple item result function.
		/// </summary>
		/// <param name="functionName"></param>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IReadOnlyCollection<TDto>> ExecuteFunctionEnumerableAsync(string functionName, object parameters = null,
			CancellationToken cancellationToken = default)
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Function(functionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			IEnumerable<TDto> results = await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
			return results.AsReadOnly();
		}

		/// <summary>
		///		Executes an action.
		/// </summary>
		/// <param name="actionName"></param>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task ExecuteActionAsync(string actionName, object parameters = null, CancellationToken cancellationToken = default)
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Executes an action for an item.
		/// </summary>
		/// <param name="actionName"></param>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task ExecuteActionAsync(string actionName, TDto dto, CancellationToken cancellationToken = default)
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Key(dto.ID)
				.Action(actionName);

			await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Executes a single item result action.
		/// </summary>
		/// <param name="actionName"></param>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TDto> ExecuteActionSingleAsync(string actionName, TDto dto, CancellationToken cancellationToken = default)
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Key(dto.ID)
				.Action(actionName);

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Executes a scalar result action.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="actionName"></param>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TResult> ExecuteActionScalar<TResult>(string actionName, object parameters = null, CancellationToken cancellationToken = default)
			where TResult : struct, IConvertible
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Executes a single item result action.
		/// </summary>
		/// <param name="actionName"></param>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<TDto> ExecuteActionSingleAsync(string actionName, object parameters = null, CancellationToken cancellationToken = default)
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///		Executes a multiple item result action.
		/// </summary>
		/// <param name="actionName"></param>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task<IReadOnlyCollection<TDto>> ExecuteActionEnumerableAsync(string actionName, object parameters = null,
			CancellationToken cancellationToken = default)
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			IEnumerable<TDto> results = await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
			return results.AsReadOnly();
		}

		private void TransferAuditValues(TDto source, TDto target)
		{
			if(source is not null && target is IAuditedObject targetAuditedObject)
			{
				IAuditedObject sourceAuditedObject = source as IAuditedObject;

				targetAuditedObject.CreatedAt = sourceAuditedObject?.CreatedAt;
				targetAuditedObject.LastModifiedAt = sourceAuditedObject?.LastModifiedAt;
				targetAuditedObject.DeletedAt = sourceAuditedObject?.DeletedAt;

				targetAuditedObject.CreatedBy = sourceAuditedObject?.CreatedBy;
				targetAuditedObject.LastModifiedBy = sourceAuditedObject?.LastModifiedBy;
				targetAuditedObject.DeletedBy = sourceAuditedObject?.DeletedBy;
			}
		}
	}
}
