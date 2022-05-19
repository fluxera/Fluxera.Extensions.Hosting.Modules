//namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
//{
//	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;

//	/// <summary>
//	///     The contract exposes only "Find" methods as an application service.
//	/// </summary>
//	/// <typeparam name="TDto">The DTO type.</typeparam>
//	public interface ICanSpatialApplicationService<TDto> : IApplicationService
//		where TDto : class, IEntityDto
//	{
//		// TODO: Move GeoPoint struct to common library.
//		///// <summary>
//		/////     Finds items near the given location.
//		///// </summary>
//		///// <param name="predicate">The predicate.</param>
//		///// <param name="locationSelector">The location property.</param>
//		///// <param name="location">The location.</param>
//		///// <param name="minDistance">The minimum distance.</param>
//		///// <param name="maxDistance">The maximum distance.</param>
//		///// <param name="queryOptions">The query options.</param>
//		///// <param name="cancellationToken">The cancellation token.</param>
//		///// <returns></returns>
//		//Task<IReadOnlyList<TDto>> FindManyNearAsync(
//		//	Expression<Func<TDto, bool>> predicate,
//		//	Expression<Func<TDto, GeoPoint>> locationSelector,
//		//	GeoPoint location,
//		//	double? minDistance = null,
//		//	double? maxDistance = null,
//		//	IQueryOptions<TDto> queryOptions = null,
//		//	CancellationToken cancellationToken = default);
//	}
//}


