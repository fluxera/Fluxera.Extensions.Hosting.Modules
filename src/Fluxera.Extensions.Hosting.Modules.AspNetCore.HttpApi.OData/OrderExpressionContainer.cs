namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System;
	using System.Linq.Expressions;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;

	internal sealed class OrderExpressionContainer<TDto>
		where TDto : class, IEntityDto
	{
		public Expression<Func<TDto, object>> Expression { get; set; }

		public bool IsDescending { get; set; }
	}
}
