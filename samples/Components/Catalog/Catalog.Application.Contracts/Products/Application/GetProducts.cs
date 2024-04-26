namespace Catalog.Application.Contracts.Products.Application
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class GetProducts : IApplicationCommand<ProductDto[]>
	{
	}
}
