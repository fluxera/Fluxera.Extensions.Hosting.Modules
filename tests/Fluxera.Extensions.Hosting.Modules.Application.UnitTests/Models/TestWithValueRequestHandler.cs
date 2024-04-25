namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using System.Threading;
	using System.Threading.Tasks;
	using MadEyeMatt.Results;
	using MediatR;

	public class TestWithValueRequestHandler : IRequestHandler<TestWithValueRequest, Result<int>>
	{
		/// <inheritdoc />
		public Task<Result<int>> Handle(TestWithValueRequest request, CancellationToken cancellationToken)
		{
			return Task.FromResult(Result.Ok(42));
		}
	}
}
