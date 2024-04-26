namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Commands
{
	using global::FluentValidation;

	public class TestCommandValidator : AbstractValidator<TestCommand>
	{
		public TestCommandValidator()
		{
			this.RuleFor(x => x.Name).NotEmpty();
		}
	}
}
