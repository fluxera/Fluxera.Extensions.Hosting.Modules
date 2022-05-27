namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     Extensions for the <see cref="ICrudApplicationService{T}" /> type.
	/// </summary>
	[PublicAPI]
	public static class CrudApplicationServiceExtensions
	{
		/// <summary>
		///     Adds the item or updates it.
		/// </summary>
		/// <typeparam name="TDto">The type of the dto.</typeparam>
		/// <param name="crudApplicationService">The crud application service.</param>
		/// <param name="dto">The dto.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		public static async Task AddOrUpdateAsync<TDto>(this ICrudApplicationService<TDto> crudApplicationService, TDto dto, CancellationToken cancellationToken = default)
			where TDto : class, IEntityDto
		{
			Guard.Against.Null(crudApplicationService);
			Guard.Against.Null(dto);

			if(dto.IsTransient())
			{
				await crudApplicationService.AddAsync(dto, cancellationToken);
			}
			else
			{
				await crudApplicationService.UpdateAsync(dto, cancellationToken);
			}
		}

		/// <summary>
		///     Adds the item or updates it.
		/// </summary>
		/// <typeparam name="TDto">The type of the dto.</typeparam>
		/// <param name="crudApplicationService">The crud application service.</param>
		/// <param name="dtos">The dtos.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		public static async Task AddOrUpdateAsync<TDto>(this ICrudApplicationService<TDto> crudApplicationService, IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
			where TDto : class, IEntityDto
		{
			Guard.Against.Null(crudApplicationService);
			dtos = Guard.Against.Null(dtos);

			foreach(TDto item in dtos)
			{
				await crudApplicationService.AddOrUpdateAsync(item, cancellationToken);
			}
		}
	}
}
