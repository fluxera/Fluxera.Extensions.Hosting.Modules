namespace Fluxera.Extensions.Hosting.Modules.ODataClient
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using Fluxera.Extensions.Http;
	using Fluxera.Extensions.OData;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	/// <summary>
	///     A base class for OData client services that read and write data.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public abstract class CrudODataClientServiceBase<T, TKey> : ReadOnlyODataClientServiceBase<T, TKey>
		where T : class, IODataEntity<TKey>
	{
		/// <inheritdoc />
		protected CrudODataClientServiceBase(string name, string collectionName, IODataClient oDataClient, RemoteService options)
			: base(name, collectionName, oDataClient, options)
		{
		}

		/// <summary>
		///     Adds the given item.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task AddAsync(T item, CancellationToken cancellationToken)
		{
			Guard.Against.Null(item);

			if(!ODataClientServiceExtensions.IsTransient(item))
			{
				throw Errors.CanNotAddExistingItem();
			}

			T result = await this.ODataClient
				.For<T>(this.CollectionName)
				.Set(item)
				.InsertEntryAsync(cancellationToken)
				.ConfigureAwait(false);

			item.ID = result.ID;
			this.TransferAuditValues(result, item);
		}

		/// <summary>
		///     Adds the given items.
		/// </summary>
		/// <param name="items"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task AddRangeAsync(IEnumerable<T> items, CancellationToken cancellationToken = default)
		{
			items = Guard.Against.Null(items);

			IList<T> instanceList = items.ToList();
			if(instanceList.Any(x => !ODataClientServiceExtensions.IsTransient(x)))
			{
				throw Errors.CanNotAddExistingItem();
			}

			ODataBatch batch = new ODataBatch(this.ODataClient);

			foreach(T dto in instanceList)
			{
				batch += async client =>
				{
					T result = await client
						.For<T>(this.CollectionName)
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

		/// <summary>
		///     Updates the given item.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task UpdateAsync(T item, CancellationToken cancellationToken)
		{
			// ReSharper disable SuspiciousTypeConversion.Global
			Guard.Against.Null(item);

			if(ODataClientServiceExtensions.IsTransient(item))
			{
				throw Errors.CanNotUpdateTransientItem();
			}

			object data = item;
			if(item is IPatchableEntityDto patchableEntityDto)
			{
				data = patchableEntityDto.ChangeTracker.GetChangesObject();
			}

			T result = await this.ODataClient
				.For<T>(this.CollectionName)
				.Key(item.ID)
				.Set(data)
				.UpdateEntryAsync(cancellationToken)
				.ConfigureAwait(false);

			this.TransferAuditValues(result, item);
			// ReSharper restore SuspiciousTypeConversion.Global
		}

		/// <summary>
		///     Updates the given items,
		/// </summary>
		/// <param name="items"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task UpdateAsync(IEnumerable<T> items, CancellationToken cancellationToken = default)
		{
			// ReSharper disable SuspiciousTypeConversion.Global
			items = Guard.Against.Null(items);

			IList<T> instanceList = items.ToList();
			if(instanceList.Any(x => ODataClientServiceExtensions.IsTransient(x)))
			{
				throw Errors.CanNotUpdateTransientItem();
			}

			ODataBatch batch = new ODataBatch(this.ODataClient);

			foreach(T dto in instanceList)
			{
				object data = dto;
				if(dto is IPatchableEntityDto patchableEntityDto)
				{
					data = patchableEntityDto.ChangeTracker.GetChangesObject();
				}

				batch += async client =>
				{
					T result = await client
						.For<T>(this.CollectionName)
						.Key(dto.ID)
						.Set(data)
						.UpdateEntryAsync(cancellationToken)
						.ConfigureAwait(false);

					this.TransferAuditValues(result, dto);
				};
			}

			await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
			// ReSharper restore SuspiciousTypeConversion.Global
		}

		/// <summary>
		///     Removes the given item.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task RemoveAsync(T item, CancellationToken cancellationToken = default)
		{
			Guard.Against.Null(item);

			if(ODataClientServiceExtensions.IsTransient(item))
			{
				throw Errors.CanNotDeleteTransientItem();
			}

			await this.ODataClient
				.For<T>(this.CollectionName)
				.Key(item.ID)
				.DeleteEntryAsync(cancellationToken)
				.ConfigureAwait(false);
		}

		/// <summary>
		///     Removes the item with the given ID.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task RemoveAsync(TKey id, CancellationToken cancellationToken)
		{
			Guard.Against.Null(id);
			Guard.Against.Default(id);

			await this.ODataClient
				.For<T>(this.CollectionName)
				.Key(id)
				.DeleteEntryAsync(cancellationToken)
				.ConfigureAwait(false);
		}

		/// <summary>
		///     Removes the given items.
		/// </summary>
		/// <param name="items"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task RemoveRangeAsync(IEnumerable<T> items, CancellationToken cancellationToken = default)
		{
			items = Guard.Against.Null(items);

			IList<T> instanceList = items.ToList();
			if(instanceList.Any(x => ODataClientServiceExtensions.IsTransient(x)))
			{
				throw Errors.CanNotDeleteTransientItem();
			}

			ODataBatch batch = new ODataBatch(this.ODataClient);

			foreach(T dto in instanceList)
			{
				batch += async client => await client
					.For<T>(this.CollectionName)
					.Key(dto.ID)
					.DeleteEntryAsync(cancellationToken)
					.ConfigureAwait(false);
			}

			await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Removes all items that satisfy the given predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public async Task RemoveRangeAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
		{
			Guard.Against.Null(predicate);

			await this.ODataClient
				.For<T>(this.CollectionName)
				.Filter(predicate)
				.DeleteEntryAsync(cancellationToken)
				.ConfigureAwait(false);
		}

		private void TransferAuditValues(T source, T target)
		{
			// ReSharper disable SuspiciousTypeConversion.Global

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

			// ReSharper enable SuspiciousTypeConversion.Global
		}
	}
}
