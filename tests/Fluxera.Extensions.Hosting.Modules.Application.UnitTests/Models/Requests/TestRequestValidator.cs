namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Requests
{
	using global::FluentValidation;

	public class TestRequestValidator : AbstractValidator<TestRequest>
	{
		public TestRequestValidator()
		{
			this.RuleFor(x => x.Name).NotEmpty();
		}
	}
}
