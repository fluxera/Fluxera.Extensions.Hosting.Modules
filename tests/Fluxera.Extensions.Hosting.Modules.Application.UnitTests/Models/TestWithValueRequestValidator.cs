namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using FluentValidation;

	public class TestWithValueRequestValidator : AbstractValidator<TestWithValueRequest>
	{
		public TestWithValueRequestValidator()
		{
			this.RuleFor(x => x.Name).NotEmpty();
		}
	}
}
