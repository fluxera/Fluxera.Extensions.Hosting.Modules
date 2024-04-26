namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Commands
{
	using global::FluentValidation;

	public class TestWithValueCommandValidator : AbstractValidator<TestWithValueCommand>
	{
		public TestWithValueCommandValidator()
		{
			this.RuleFor(x => x.Name).NotEmpty();
		}
	}
}
