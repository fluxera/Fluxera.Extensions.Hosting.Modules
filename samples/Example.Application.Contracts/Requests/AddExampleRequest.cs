namespace Example.Application.Contracts.Requests
{
	using Example.Application.Contracts.Dtos;
	using FluentResults;
	using JetBrains.Annotations;
	using MediatR;

	[PublicAPI]
	public sealed class AddExampleRequest : IRequest<Result<ExampleDto>>
	{
		public AddExampleRequest(ExampleDto exampleDto)
		{
			this.ExampleDto = exampleDto;
		}

		public ExampleDto ExampleDto { get; }
	}
}
