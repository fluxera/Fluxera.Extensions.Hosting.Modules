namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using FluentValidation;

	public class TestWithoutResultRequestValidator : AbstractValidator<TestWithoutResultRequest>
	{
		public TestWithoutResultRequestValidator()
		{
			this.RuleFor(x => x.Name).NotEmpty();
		}
	}
}
