namespace Catalog.Application.Handlers
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Catalog.Application.Contracts.Dtos;
	using Catalog.Application.Contracts.Requests;
	using Catalog.Domain.Example;
	using Catalog.Domain.Shared.Example;
	using FluentResults;
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using MediatR;

	[UsedImplicitly]
	internal sealed class GetExampleRequestHandler : IRequestHandler<GetExampleRequest, Result<ExampleDto>>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Example, ExampleId> repository;

		public GetExampleRequestHandler(IExampleRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ExampleDto>> Handle(GetExampleRequest request, CancellationToken cancellationToken)
		{
			Result<ExampleDto> result;

			try
			{
				ExampleId id = request.ExampleId;
				Example entity = await this.repository.FindOneAsync(x => x.ID == id, cancellationToken: cancellationToken);
				ExampleDto dto = this.mapper.Map<ExampleDto>(entity);

				result = Result.Ok(dto);
			}
			catch(Exception ex)
			{
				result = Result.Fail<ExampleDto>(ex.Message);
			}

			return result;
		}
	}
}
