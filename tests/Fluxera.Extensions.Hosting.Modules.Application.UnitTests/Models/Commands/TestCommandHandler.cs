namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Commands
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Handlers;
	using MadEyeMatt.Results;

	public class TestCommandHandler : IApplicationCommandHandler<TestCommand>
	{
		/// <inheritdoc />
		public Task<Result> HandleAsync(TestCommand request, CancellationToken cancellationToken)
		{
			return Task.FromResult(Result.Ok());
		}
	}
}
