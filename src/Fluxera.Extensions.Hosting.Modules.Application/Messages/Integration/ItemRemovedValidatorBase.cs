namespace Fluxera.Extensions.Hosting.Modules.Application.Messages.Integration
{
	using FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration;
	using JetBrains.Annotations;

	/// <summary>
	///		An abstract base class for <see cref="ItemRemoved"/> message validators.
	/// </summary>
	[PublicAPI]
	public abstract class ItemRemovedValidatorBase : AbstractValidator<ItemRemoved>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="ItemRemovedValidatorBase"/> type.
		/// </summary>
		protected ItemRemovedValidatorBase()
		{
			this.RuleFor(x => x.Metadata)
				.NotEmpty()
				.SetValidator(new ItemRemovedDataValidator());
		}
	}
}