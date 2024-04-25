namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using MediatR;

	public class TestWithoutResultRequest : IRequest<int>
	{
		public string Name { get; set; }
	}
}
