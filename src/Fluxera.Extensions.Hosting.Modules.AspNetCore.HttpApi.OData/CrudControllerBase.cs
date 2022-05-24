namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using Fluxera.Extensions.Validation;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.OData.Deltas;
	using Microsoft.AspNetCore.OData.Formatter;

	/// <summary>
	///     A base controller for OData CRUD operations.
	/// </summary>
	/// <typeparam name="TDto"></typeparam>
	[PublicAPI]
	public abstract class CrudControllerBase<TDto> : ReadOnlyCrudControllerBase<TDto>
		where TDto : class, IEntityDto
	{
		private readonly ICrudApplicationService<TDto> applicationService;

		/// <inheritdoc />
		protected CrudControllerBase(ICrudApplicationService<TDto> applicationService)
			: base(applicationService)
		{
			this.applicationService = applicationService;
		}

		/// <summary>
		///     Flag, indicating if the controller supports the insert operation.
		/// </summary>
		protected virtual bool SupportsInsert => true;

		/// <summary>
		///     Flag, indicating if the controller supports the update operation.
		/// </summary>
		protected virtual bool SupportsUpdate => true;

		/// <summary>
		///     Flag, indicating if the controller supports the delete operation.
		/// </summary>
		protected virtual bool SupportsDelete => true;

		// POST: odata/{Items}
		/// <summary>
		///     Insert the given item.
		/// </summary>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost]
		[ServiceFilter(typeof(IdempotentTokenFilter))]
		public virtual async Task<IActionResult> Post([FromBody] TDto dto, CancellationToken cancellationToken = default)
		{
			if(!this.SupportsInsert)
			{
				return this.NotFound();
			}

			if(this.ModelState.IsValid)
			{
				// Add the new item.
				try
				{
					await this.applicationService.AddAsync(dto, cancellationToken);
					return this.Created(dto);
				}
				catch(NotSupportedException)
				{
					return this.NotFound();
				}
				catch(ValidationException ex)
				{
					this.ModelState.AddModelErrors(ex);
				}
			}

			return this.ValidationProblem();
		}

		// PUT: odata/{Items}(5)
		/// <summary>
		///     Update (by replacing) the given item.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="dto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPut]
		public virtual async Task<IActionResult> Put([FromODataUri] string key, [FromBody] TDto dto, CancellationToken cancellationToken = default)
		{
			if(!this.SupportsUpdate)
			{
				return this.NotFound();
			}

			if(this.ModelState.IsValid)
			{
				// Check if the item exists.
				try
				{
					bool exists = await this.applicationService.ExistsAsync(key, cancellationToken);
					if(!exists)
					{
						return this.NotFound();
					}

					// Get the item to preserve values that should not be changed.
					TDto item = await this.applicationService.GetAsync(key, cancellationToken);

					// Preserve change audit properties.
					if(item is IAuditedObject sourceAuditedObject && dto is IAuditedObject targetAuditedObject)
					{
						targetAuditedObject.CreatedAt = sourceAuditedObject.CreatedAt;
						targetAuditedObject.LastModifiedAt = sourceAuditedObject.LastModifiedAt;
						targetAuditedObject.DeletedAt = sourceAuditedObject.DeletedAt;

						targetAuditedObject.CreatedBy = sourceAuditedObject.CreatedBy;
						targetAuditedObject.LastModifiedBy = sourceAuditedObject.LastModifiedBy;
						targetAuditedObject.DeletedBy = sourceAuditedObject.DeletedBy;
					}

					// Preserve tenant property.
					if(item is IMultiTenancyObject sourceMultiTenancyObject && dto is IMultiTenancyObject targetMultiTenancyObject)
					{
						targetMultiTenancyObject.TenantID = sourceMultiTenancyObject.TenantID;
					}

					// Save the updated item.
					try
					{
						await this.applicationService.UpdateAsync(dto, cancellationToken);
						return this.Updated(dto);
					}
					catch(ValidationException ex)
					{
						this.ModelState.AddModelErrors(ex);
					}
				}
				catch(NotSupportedException)
				{
					return this.NotFound();
				}
			}

			return this.ValidationProblem();
		}

		// PATCH: odata/{Items}(5)
		// MERGE: odata/{Items}(5)
		/// <summary>
		///     Update (by patching) the given item delta to the existing item.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="delta"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPatch]
		[HttpMerge]
		public virtual async Task<IActionResult> Patch([FromODataUri] string key, [FromBody] Delta<TDto> delta, CancellationToken cancellationToken = default)
		{
			if(!this.SupportsUpdate)
			{
				return this.NotFound();
			}

			if(this.ModelState.IsValid)
			{
				// Check if the item exists.
				try
				{
					bool exists = await this.applicationService.ExistsAsync(key, cancellationToken);
					if(!exists)
					{
						return this.NotFound();
					}

					// Get the item and patch it with the changed values.
					TDto item = await this.applicationService.GetAsync(key, cancellationToken);

					// Preserve values that should not be changed by removing the properties from the delta.
					Delta<TDto> newDelta = new Delta<TDto>();

					foreach(string propertyName in delta.GetChangedPropertyNames())
					{
						// Preserve change audit and tenant properties.
						if(propertyName != nameof(IAuditedObject.CreatedAt) &&
						   propertyName != nameof(IAuditedObject.LastModifiedAt) &&
						   propertyName != nameof(IAuditedObject.DeletedAt) &&
						   propertyName != nameof(IAuditedObject.CreatedBy) &&
						   propertyName != nameof(IAuditedObject.LastModifiedBy) &&
						   propertyName != nameof(IAuditedObject.DeletedBy) &&
						   propertyName != nameof(IMultiTenancyObject.TenantID))
						{
							if(delta.TryGetPropertyValue(propertyName, out object fieldValue))
							{
								newDelta.TrySetPropertyValue(propertyName, fieldValue);
							}
						}
					}

					// Patch the existing item.
					newDelta.Patch(item);

					// Save the patched item.
					try
					{
						await this.applicationService.UpdateAsync(item, cancellationToken);
						return this.Updated(item);
					}
					catch(ValidationException ex)
					{
						this.ModelState.AddModelErrors(ex);
					}
				}
				catch(NotSupportedException)
				{
					return this.NotFound();
				}
			}

			return this.ValidationProblem();
		}

		// DELETE: odata/{Items}(5)
		/// <summary>
		///     Delete the item identified by the given key.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpDelete]
		public virtual async Task<IActionResult> Delete([FromODataUri] string key, CancellationToken cancellationToken = default)
		{
			if(!this.SupportsDelete)
			{
				return this.NotFound();
			}

			// Check if the item exists.
			try
			{
				bool exists = await this.applicationService.ExistsAsync(key, cancellationToken);
				if(!exists)
				{
					return this.NotFound();
				}

				// Delete the item.
				await this.applicationService.RemoveAsync(key, cancellationToken);
				return this.NoContent();
			}
			catch(NotSupportedException)
			{
				return this.NotFound();
			}
		}
	}
}
