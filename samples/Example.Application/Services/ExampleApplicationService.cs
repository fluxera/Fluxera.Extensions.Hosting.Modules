namespace Catalog.Application.Services
{
	using System.Threading.Tasks;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Application.Contracts.Requests;
	using Catalog.Application.Contracts.Services;
	using Catalog.Domain.Shared.Example;
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
