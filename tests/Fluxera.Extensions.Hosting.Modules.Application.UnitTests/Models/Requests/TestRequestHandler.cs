namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Requests
{
	using System.Threading;
	using System.Threading.Tasks;
	using global::MediatR;

	public class TestRequestHandler : IRequestHandler<TestRequest, int>
	{
		/// <inheritdoc />
		public Task<int> Handle(TestRequest request, CancellationToken cancellationToken)
		{
			return Task.FromResult(42);
		}
	}
}
