namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using MadEyeMatt.Results;
	using MediatR;

	public class TestRequest : IRequest<Result>
	{
		public string Name { get; set; }
	}
}
