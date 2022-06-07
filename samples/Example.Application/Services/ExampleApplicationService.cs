namespace Example.Application.Services
{
	using System.Threading.Tasks;
	using AutoMapper;
	using Example.Application.Contracts.Dtos;
	using Example.Application.Contracts.Services;
	using Example.Domain.ExampleAggregate.Model;
	using Example.Domain.ExampleAggregate.Repositories;
	using Example.Domain.Shared.ExampleAggregate.Model;
	using Fluxera.Repository;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ExampleApplicationService : IExampleApplicationService
	{
		private readonly IMapper mapper;
		private readonly IRepository<Example, ExampleId> repository;

		public ExampleApplicationService(IExampleRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		/// <inheritdoc />
		public async Task<ExampleDto> GetExampleAsync(ExampleId id)
		{
			Example entity = await this.repository.FindOneAsync(x => x.ID == id);
			ExampleDto dto = this.mapper.Map<ExampleDto>(entity);

			return dto;
		}

		/// <inheritdoc />
		public async Task<ExampleDto> AddExample(ExampleDto item)
		{
			Example entity = this.mapper.Map<Example>(item);
			await this.repository.AddAsync(entity);
			ExampleDto dto = this.mapper.Map<ExampleDto>(entity);

			return dto;
		}
	}
}
