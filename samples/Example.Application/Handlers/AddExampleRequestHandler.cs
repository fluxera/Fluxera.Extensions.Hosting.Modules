namespace Example.Application.Handlers
{
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Example.Application.Contracts.Dtos;
	using Example.Application.Contracts.Requests;
	using Example.Domain.Example;
	using Example.Domain.Shared.Example;
	using Fluxera.Repository;
	using JetBrains.Annotations;
	using MediatR;

	[UsedImplicitly]
	internal sealed class AddExampleRequestHandler : IRequestHandler<AddExampleRequest, ExampleDto>
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
		public async Task<ExampleDto> Handle(AddExampleRequest request, CancellationToken cancellationToken)
		{
			Example example = this.mapper.Map<Example>(request.ExampleDto);
			await this.repository.AddAsync(example, cancellationToken);
			await this.unitOfWork.SaveChangesAsync(cancellationToken);
			ExampleDto exampleDto = this.mapper.Map<ExampleDto>(example);

			return exampleDto;
		}
	}
}
