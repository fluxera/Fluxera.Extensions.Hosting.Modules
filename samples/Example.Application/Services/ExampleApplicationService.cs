namespace Example.Application.Services
{
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using Example.Application.Contracts.Requests;
	using Example.Application.Contracts.Services;
	using Example.Domain.Shared.Example;
	using FluentResults;
	using JetBrains.Annotations;
	using MediatR;

	[UsedImplicitly]
	internal sealed class ExampleApplicationService : IExampleApplicationService
	{
		private readonly ISender sender;

		public ExampleApplicationService(ISender sender)
		{
			this.sender = sender;
		}

		/// <inheritdoc />
		public Task<Result<ExampleDto>> GetExampleAsync(ExampleId id)
		{
			return this.sender.Send(new GetExampleRequest(id));
		}

		/// <inheritdoc />
		public Task<Result<ExampleDto>> AddExample(ExampleDto dto)
		{
			return this.sender.Send(new AddExampleRequest(dto));
		}
	}
}
