namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <inheritdoc cref="ICanGetApplicationService{TDto,TKey}" />
	/// <inheritdoc cref="ICanFindApplicationService{TDto,TKey}" />
	/// <inheritdoc cref="ICanAggregateApplicationService{TDto,TKey}" />
	/// <summary>
	///     Contract for an application services that reads <typeparamref name="TDto" /> instances.
	/// </summary>
	/// <typeparam name="TDto">The DTO type.</typeparam>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	[PublicAPI]
	public interface IReadOnlyCrudApplicationService<TDto, TKey> : ICrudApplicationService,
		ICanGetApplicationService<TDto, TKey>,
		ICanFindApplicationService<TDto, TKey>,
		ICanAggregateApplicationService<TDto, TKey>
		where TDto : class, IEntityDto<TKey>
		where TKey : notnull, IComparable<TKey>, IEquatable<TKey>
	{
	}
}
