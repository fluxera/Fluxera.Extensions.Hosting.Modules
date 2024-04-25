namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;

	public class TestWithoutResultRequestHandler : IRequestHandler<TestWithoutResultRequest, int>
	{
		/// <inheritdoc />
		public Task<int> Handle(TestWithoutResultRequest request, CancellationToken cancellationToken)
		{
			return Task.FromResult(42);
		}
	}
}
