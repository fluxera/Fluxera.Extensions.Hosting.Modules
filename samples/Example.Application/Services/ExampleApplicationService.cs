namespace Example.Application.Services
{
	using System.Threading.Tasks;
	using Example.Application.Contracts.Dtos;
	using Example.Application.Contracts.Services;
	using Example.Domain.Shared.Example;
	using JetBrains.Annotations;
	using MediatR;

	[UsedImplicitly]
	internal sealed class ExampleApplicationService : IExampleApplicationService
	{
		// https://github.com/jbogard/MediatR/wiki
		private readonly ISender sender;

		public ExampleApplicationService(ISender sender)
		{
			this.sender = sender;
		}

		/// <inheritdoc />
		public Task<ExampleDto> GetExampleAsync(ExampleId id)
		{
			return this.sender.Send(new GetExampleRequest(id));
		}

		/// <inheritdoc />
		public Task<ExampleDto> AddExample(ExampleDto item)
		{
			return this.sender.Send(new AddExampleRequest());
		}
	}
}
