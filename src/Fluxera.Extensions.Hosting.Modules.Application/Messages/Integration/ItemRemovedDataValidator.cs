namespace Fluxera.Extensions.Hosting.Modules.Application.Messages.Integration
{
	using FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ItemRemovedDataValidator : AbstractValidator<ItemRemovedData>
	{
		public ItemRemovedDataValidator()
		{
			this.RuleFor(x => x.EntityID).NotEmpty();

			this.RuleFor(x => x.EntityName).NotEmpty();

			this.RuleFor(x => x.EntityLongName).NotEmpty();

			this.RuleFor(x => x.DeletedAt).NotNull();

			this.RuleFor(x => x.BeforeDeletedState).NotEmpty();
		}
	}
}