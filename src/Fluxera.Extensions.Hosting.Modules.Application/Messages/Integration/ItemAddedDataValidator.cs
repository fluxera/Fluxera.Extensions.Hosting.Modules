namespace Fluxera.Extensions.Hosting.Modules.Application.Messages.Integration
{
	using FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ItemAddedDataValidator : AbstractValidator<ItemAddedData>
	{
		public ItemAddedDataValidator()
		{
			this.RuleFor(x => x.EntityID).NotEmpty();

			this.RuleFor(x => x.EntityName).NotEmpty();

			this.RuleFor(x => x.EntityLongName).NotEmpty();

			this.RuleFor(x => x.CreatedAt).NotNull();

			this.RuleFor(x => x.AfterCreatedState).NotEmpty();
		}
	}
}