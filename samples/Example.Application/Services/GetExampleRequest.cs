namespace Example.Application.Services
{
	using Example.Application.Contracts.Dtos;
	using Example.Domain.Shared.Example;
	using MediatR;

	public sealed class GetExampleRequest : IRequest<ExampleDto>
	{
		public GetExampleRequest(ExampleId exampleId)
		{
			this.ExampleId = exampleId;
		}

		public ExampleId ExampleId { get; }
	}
}
