namespace Example.Application.Contracts.Services
{
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using Example.Domain.Shared.Example;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IExampleApplicationService : IApplicationService
	{
		Task<Result<ExampleDto>> GetExampleAsync(ExampleId id);

		Task<Result<ExampleDto>> AddExample(ExampleDto dto);
	}
}
