namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Requests
{
	using global::MediatR;

	public class TestRequest : IRequest<int>
	{
		public string Name { get; set; }
	}
}
