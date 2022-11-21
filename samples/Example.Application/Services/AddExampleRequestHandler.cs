namespace Example.Application.Services
{
	using System.Threading;
	using System.Threading.Tasks;
	using AutoMapper;
	using Example.Application.Contracts.Dtos;
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

		public AddExampleRequestHandler(IExampleRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<ExampleDto> Handle(AddExampleRequest request, CancellationToken cancellationToken)
		{
			Example entity = this.mapper.Map<Example>(request);
			await this.repository.AddAsync(entity, cancellationToken);
			ExampleDto dto = this.mapper.Map<ExampleDto>(entity);

			return dto;
		}
	}
}
