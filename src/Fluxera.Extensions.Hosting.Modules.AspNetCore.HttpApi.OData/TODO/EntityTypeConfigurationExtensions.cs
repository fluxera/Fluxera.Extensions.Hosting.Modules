//namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.OData
//{
//	using System;
//	using System.Linq.Expressions;
//	using JetBrains.Annotations;
//	using Microsoft.OData.ModelBuilder;

//	[PublicAPI]
//	public static class EntityTypeConfigurationExtensions
//	{
//		public static EntityTypeConfiguration<TDto> HasID<TDto>(
//			this EntityTypeConfiguration<TDto> entityType,
//			Expression<Func<TDto, string>> keyDefinitionExpression)
//			where TDto : class, IEntityDto
//		{
//			entityType.HasKey(keyDefinitionExpression)
//				.Property(keyDefinitionExpression)
//				.IsOptional()
//				.IsConcurrencyToken();

//			return entityType;
//		}

//		// https://github.com/OData/ODataSamples/tree/master/WebApiCore/ODataEtagSample<
//		public static EntityTypeConfiguration<TDto> IsAudited<TDto>(
//			this EntityTypeConfiguration<TDto> entityType)
//			where TDto : class, IAuditedObject
//		{
//			entityType.Property(x => x.CreatedAt)
//				.IsConcurrencyToken();

//			entityType.Property(x => x.LastModifiedAt)
//				.IsConcurrencyToken();

//			return entityType;
//		}
//	}
//}


