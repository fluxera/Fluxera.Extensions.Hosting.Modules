namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <inheritdoc cref="ICanAddApplicationService{TDto,TKey}" />
	/// <inheritdoc cref="ICanUpdateApplicationService{TDto,TKey}" />
	/// <inheritdoc cref="ICanRemoveApplicationService{TDto,TKey}" />
	/// <inheritdoc cref="IReadOnlyCrudApplicationService{TDto,TKey}" />
	/// <summary>
	///     Contract for an application services that reads and writes <typeparamref name="TDto" /> instances.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	[PublicAPI]
	public interface ICrudApplicationService<TDto, TKey> : ICrudApplicationService,
		ICanAddApplicationService<TDto, TKey>,
		ICanUpdateApplicationService<TDto, TKey>,
		ICanRemoveApplicationService<TDto, TKey>,
		IReadOnlyCrudApplicationService<TDto, TKey>
		where TDto : class, IEntityDto<TKey>
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
	}

	/// <summary>
	///     Marker interface for a special CRUD application service.
	/// </summary>
	[PublicAPI]
	public interface ICrudApplicationService : IApplicationService
	{
	}
}
