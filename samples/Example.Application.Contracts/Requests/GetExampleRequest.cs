namespace Example.Application.Contracts.Requests
{
	using Example.Application.Contracts.Dtos;
	using Example.Domain.Shared.Example;
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
