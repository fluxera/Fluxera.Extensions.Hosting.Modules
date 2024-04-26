namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Commands
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Handlers;
	using MadEyeMatt.Results;

	public class TestWithValueCommandHandler : IApplicationCommandHandler<TestWithValueCommand, int>
	{
		/// <inheritdoc />
		public Task<Result<int>> HandleAsync(TestWithValueCommand request, CancellationToken cancellationToken)
		{
			return Task.FromResult(Result.Ok(42));
		}
	}
}
