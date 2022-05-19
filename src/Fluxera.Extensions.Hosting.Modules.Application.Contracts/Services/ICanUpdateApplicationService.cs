//namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
//{
//	using System.Collections.Generic;
//	using System.Threading;
//	using System.Threading.Tasks;
//	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
//	using JetBrains.Annotations;

//	/// <summary>
//	///     The contract exposes only "Update" methods as an application service.
//	/// </summary>
//	/// <typeparam name="TDto">The DTO type.</typeparam>
//	[PublicAPI]
//	public interface ICanUpdateApplicationService<in TDto> : IApplicationService
//		where TDto : class, IEntityDto
//	{
//		Task UpdateAsync(TDto dto, CancellationToken cancellationToken = default);

//		Task UpdateAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken = default);
//	}
//}


