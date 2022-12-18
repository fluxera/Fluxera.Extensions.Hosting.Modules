namespace Catalog.Application.Contracts.Requests
{
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Domain.Shared.Example;
	using FluentResults;
	using JetBrains.Annotations;
	using MediatR;

	[PublicAPI]
	public sealed class GetExampleRequest : IRequest<Result<ExampleDto>>
	{
		public GetExampleRequest(ExampleId exampleId)
		{
			this.ExampleId = exampleId;
		}

		public ExampleId ExampleId { get; }
	}
}
