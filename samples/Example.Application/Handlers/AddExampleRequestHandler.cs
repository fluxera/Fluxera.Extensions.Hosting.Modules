namespace Example.Application.Handlers
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Example.Application.Contracts.Dtos;
	using Example.Application.Contracts.Requests;
	using Example.Domain.Example;
	using Example.Domain.Shared.Example;
	using FluentResults;
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using MediatR;

	[UsedImplicitly]
	internal sealed class AddExampleRequestHandler : IRequestHandler<AddExampleRequest, Result<ExampleDto>>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Example, ExampleId> repository;
		private readonly IUnitOfWork unitOfWork;

		public AddExampleRequestHandler(
			IExampleRepository repository,
			IUnitOfWorkFactory unitOfWorkFactory,
			IMapper mapper)
		{
			this.repository = repository;
			this.unitOfWork = unitOfWorkFactory.CreateUnitOfWork(repository.RepositoryName);
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<Result<ExampleDto>> Handle(AddExampleRequest request, CancellationToken cancellationToken)
		{
			Result<ExampleDto> result;

			try
			{
				Example example = this.mapper.Map<Example>(request.ExampleDto);
				await this.repository.AddAsync(example, cancellationToken);
				await this.unitOfWork.SaveChangesAsync(cancellationToken);
				ExampleDto dto = this.mapper.Map<ExampleDto>(example);

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
