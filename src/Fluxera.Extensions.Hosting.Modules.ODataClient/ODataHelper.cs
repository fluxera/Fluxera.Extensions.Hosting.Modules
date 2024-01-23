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

	[PublicAPI]
	public sealed class ODataHelper<TDto, TKey>
		where TDto : class, IEntityDto<TKey> 
		where TKey : IComparable<TKey>, IEquatable<TKey>
	{
		private readonly IODataClient oDataClient;
		private readonly string collectionName;

		/// <summary>
		///		Initializes a new instance of the <see cref="ODataHelper{TDto, TKey}"/> type.
		/// </summary>
		/// <param name="oDataClient"></param>
		/// <param name="collectionName"></param>
		public ODataHelper(IODataClient oDataClient, string collectionName)
		{
			this.oDataClient = oDataClient;
			this.collectionName = collectionName;
		}

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

					if(result != null)
					{
						dto.ID = result.ID;
						this.TransferAuditValues(result, dto);
					}
				};
			}

			await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		public async Task UpdateAsync(TDto dto, CancellationToken cancellationToken)
		{
			dto = Guard.Against.Null(dto);

			if(dto.IsTransient())
			{
				throw Errors.CanNotUpdateTransientItem();
			}

			object data = dto;
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

		public async Task DeleteAsync(string id, CancellationToken cancellationToken)
		{
			id = Guard.Against.NullOrWhiteSpace(id);

			await this.oDataClient
				.For<TDto>(this.collectionName)
				.Key(id)
				.DeleteEntryAsync(cancellationToken)
				.ConfigureAwait(false);
		}

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

		public async Task DeleteRangeAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			predicate = Guard.Against.Null(predicate);

			await this.oDataClient
				.For<TDto>(this.collectionName)
				.Filter(predicate)
				.DeleteEntryAsync(cancellationToken)
				.ConfigureAwait(false);
		}

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

		public async Task<TResult> GetAsync<TResult>(string id, Expression<Func<TDto, TResult>> selector, CancellationToken cancellationToken = default)
		{
			try
			{
				TDto item = await this.oDataClient
					.For<TDto>(this.collectionName)
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

		public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken)
		{
			TDto result = await this.GetAsync(id, cancellationToken).ConfigureAwait(false);
			return result != null;
		}

		public async Task<bool> ExistsAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default)
		{
			long count = await this.CountAsync(predicate, cancellationToken).ConfigureAwait(false);
			return count > 0;
		}

		public async Task<long> CountAsync(CancellationToken cancellationToken = default)
		{
			return await this.oDataClient
				.For<TDto>(this.collectionName)
				.Count()
				.FindScalarAsync<long>(cancellationToken)
				.ConfigureAwait(false);
		}

		public async Task<long> CountAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken)
		{
			return await this.oDataClient
				.For<TDto>(this.collectionName)
				.Filter(predicate)
				.Count()
				.FindScalarAsync<long>(cancellationToken)
				.ConfigureAwait(false);
		}

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

		public async Task<TResult> ExecuteFunctionScalarAsync<TResult>(string functionName, object parameters = null, CancellationToken cancellationToken = default)
			where TResult : struct, IConvertible//, INumber<TResult>
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

		public async Task<IReadOnlyCollection<TDto>> ExecuteFunctionEnumerableAsync(string functionName, object parameters = null, CancellationToken cancellationToken = default)
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

		public async Task ExecuteActionAsync(string actionName, TDto dto, CancellationToken cancellationToken = default)
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Key(dto.ID)
				.Action(actionName);

			await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		public async Task<TDto> ExecuteActionSingleAsync(string actionName, TDto dto, CancellationToken cancellationToken = default)
		{
			IBoundClient<TDto> boundClient = this.oDataClient
				.For<TDto>(this.collectionName)
				.Key(dto.ID)
				.Action(actionName);

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

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

		public async Task<IReadOnlyCollection<TDto>> ExecuteActionEnumerableAsync(string actionName, object parameters = null, CancellationToken cancellationToken = default)
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
