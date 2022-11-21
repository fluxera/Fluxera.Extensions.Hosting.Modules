namespace Example.Application.Services
{
	using Example.Application.Contracts.Dtos;
	using MediatR;

	public sealed class AddExampleRequest : IRequest<ExampleDto>
	{
	}
}
