namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using FluentValidation;

	public class TestRequestValidator : AbstractValidator<TestRequest>
	{
		public TestRequestValidator()
		{
			this.RuleFor(x => x.Name).NotEmpty();
		}
	}
}
