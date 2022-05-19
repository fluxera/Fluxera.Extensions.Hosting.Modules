//namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
//{
//	using System;
//	using System.Collections.Generic;
//	using System.Linq.Expressions;
//	using System.Threading;
//	using System.Threading.Tasks;
//	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
//	using JetBrains.Annotations;

//	/// <summary>
//	///     The contract exposes only "Delete" methods as an application service.
//	/// </summary>
//	/// <typeparam name="TDto">The DTO type.</typeparam>
//	[PublicAPI]
//	public interface ICanDeleteApplicationService<TDto> : IApplicationService
//		where TDto : class, IEntityDto
//	{
//		Task DeleteAsync(TDto dto, CancellationToken cancellationToken = default);

//		Task DeleteAsync(string id, CancellationToken cancellationToken = default);

//		Task DeleteAsync(Expression<Func<TDto, bool>> predicate, CancellationToken cancellationToken = default);

//		Task DeleteAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default);
//	}
//}


