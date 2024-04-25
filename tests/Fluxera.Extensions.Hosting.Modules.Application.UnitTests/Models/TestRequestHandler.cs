namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using System.Threading;
	using System.Threading.Tasks;
	using MadEyeMatt.Results;
	using MediatR;

	public class TestRequestHandler : IRequestHandler<TestRequest, Result>
	{
		/// <inheritdoc />
		public Task<Result> Handle(TestRequest request, CancellationToken cancellationToken)
		{
			return Task.FromResult(Result.Ok());
		}
	}
}
