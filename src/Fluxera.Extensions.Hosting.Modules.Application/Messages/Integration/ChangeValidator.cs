namespace Fluxera.Extensions.Hosting.Modules.Application.Messages.Integration
{
	using FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ChangeValidator : AbstractValidator<Change>
	{
		public ChangeValidator()
		{
			this.RuleFor(x => x.Path).NotEmpty();

			this.RuleFor(x => x.BeforeUpdateValue).NotNull();

			this.RuleFor(x => x.AfterUpdateValue).NotNull();

			this.RuleFor(x => x.DataType).NotNull();
		}
	}
}
