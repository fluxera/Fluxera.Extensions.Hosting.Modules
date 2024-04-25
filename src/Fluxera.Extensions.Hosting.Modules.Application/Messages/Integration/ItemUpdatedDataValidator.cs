namespace Fluxera.Extensions.Hosting.Modules.Application.Messages.Integration
{
	using FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ItemUpdatedDataValidator : AbstractValidator<ItemUpdatedData>
	{
		public ItemUpdatedDataValidator()
		{
			this.RuleFor(x => x.EntityID).NotEmpty();

			this.RuleFor(x => x.EntityName).NotEmpty();

			this.RuleFor(x => x.EntityLongName).NotEmpty();

			this.RuleFor(x => x.LastModifiedAt).NotNull();

			this.RuleFor(x => x.BeforeUpdateState).NotEmpty();

			this.RuleFor(x => x.AfterUpdateState).NotEmpty();

			this.RuleFor(x => x.Changes).NotEmpty();

			this.RuleForEach(x => x.Changes).SetValidator(new ChangeValidator());
		}
	}
}