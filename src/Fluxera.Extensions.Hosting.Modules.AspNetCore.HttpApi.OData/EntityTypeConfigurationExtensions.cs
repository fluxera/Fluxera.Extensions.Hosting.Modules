namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System;
	using System.Linq.Expressions;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using JetBrains.Annotations;
	using Microsoft.OData.ModelBuilder;

	/// <summary>
	///     Extension method for the <see cref="EntityTypeConfiguration{TEntityType}" /> type.
	/// </summary>
	[PublicAPI]
	public static class EntityTypeConfigurationExtensions
	{
		/// <summary>
		///     Configures the property to be an optional key.
		/// </summary>
		/// <typeparam name="TDto"></typeparam>
		/// <param name="entityType"></param>
		/// <param name="keyDefinitionExpression"></param>
		/// <returns></returns>
		public static EntityTypeConfiguration<TDto> HasID<TDto>(
			this EntityTypeConfiguration<TDto> entityType,
			Expression<Func<TDto, string>> keyDefinitionExpression)
			where TDto : class, IEntityDto
		{
			entityType.HasKey(keyDefinitionExpression)
				.Property(keyDefinitionExpression)
				.IsNullable()
				.IsConcurrencyToken();

			return entityType;
		}

		/// <summary>
		///     Configures the entity type to be audited.
		/// </summary>
		/// <remarks>
		///     https://github.com/OData/ODataSamples/tree/master/WebApiCore/ODataEtagSample
		/// </remarks>
		/// <typeparam name="TDto"></typeparam>
		/// <param name="entityType"></param>
		/// <returns></returns>
		public static EntityTypeConfiguration<TDto> IsAudited<TDto>(
			this EntityTypeConfiguration<TDto> entityType)
			where TDto : class, IAuditedObject
		{
			entityType.Property(x => x.CreatedAt)
				.IsConcurrencyToken();

			entityType.Property(x => x.LastModifiedAt)
				.IsConcurrencyToken();

			return entityType;
		}
	}
}
