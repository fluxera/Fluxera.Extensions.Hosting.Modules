namespace Fluxera.Extensions.Hosting.Modules.Application.Messages.Integration
{
	using FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration;
	using JetBrains.Annotations;

	/// <summary>
	///		An abstract base class for <see cref="ItemUpdated"/> message validators.
	/// </summary>
	[PublicAPI]
	public abstract class ItemUpdatedValidatorBase : AbstractValidator<ItemUpdated>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="ItemUpdatedValidatorBase"/> type.
		/// </summary>
		protected ItemUpdatedValidatorBase()
		{
			this.RuleFor(x => x.Metadata)
				.NotEmpty()
				.SetValidator(new ItemUpdatedDataValidator());
		}
	}
}