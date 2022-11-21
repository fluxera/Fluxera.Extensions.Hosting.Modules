namespace Example.Application.Contracts.Services
{
	using Example.Application.Contracts.Dtos;
	using JetBrains.Annotations;
	using MediatR;

	[PublicAPI]
	public sealed class AddExampleRequest : IRequest<ExampleDto>
	{
		public AddExampleRequest(ExampleDto exampleDto)
		{
			this.ExampleDto = exampleDto;
		}

		public ExampleDto ExampleDto { get; }
	}
}
