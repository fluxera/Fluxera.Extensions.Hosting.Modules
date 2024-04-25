namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using MadEyeMatt.Results;
	using MediatR;

	public class TestWithValueRequest : IRequest<Result<int>>
	{
		public string Name { get; set; }
	}
}
