namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Events
{
	using global::FluentValidation;

	public class TestEventValidator : AbstractValidator<TestEvent>
	{
		public TestEventValidator()
		{
			this.RuleFor(x => x.Name).NotEmpty();
		}
	}
}
