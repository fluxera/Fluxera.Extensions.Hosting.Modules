namespace Fluxera.Extensions.Hosting.Modules.ODataClient
{
	using System;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Simple.OData.Client;

	internal static class ODataQueryHelpers
	{
		internal static IBoundClient<TDto> ApplyOptions<TDto>(this IBoundClient<TDto> client, Func<IBoundClient<TDto>, IBoundClient<TDto>> queryOptions) 
			where TDto : class, IEntityDto
		{
			return queryOptions?.Invoke(client);
		}
	}
}
