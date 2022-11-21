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
	internal sealed class GetExampleRequestHandler : IRequestHandler<GetExampleRequest, ExampleDto>
	{
		private readonly IMapper mapper;
		private readonly IRepository<Example, ExampleId> repository;

		public GetExampleRequestHandler(IExampleRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<ExampleDto> Handle(GetExampleRequest request, CancellationToken cancellationToken)
		{
			ExampleId id = request.ExampleId;
			Example entity = await this.repository.FindOneAsync(x => x.ID == id, cancellationToken: cancellationToken);
			ExampleDto dto = this.mapper.Map<ExampleDto>(entity);

			return dto;
		}
	}
}
