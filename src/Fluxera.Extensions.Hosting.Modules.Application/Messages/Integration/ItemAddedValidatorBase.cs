namespace Fluxera.Extensions.Hosting.Modules.Application.Messages.Integration
{
	using FluentValidation;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Messages.Integration;
	using JetBrains.Annotations;

	/// <summary>
	///		An abstract base class for <see cref="ItemAdded"/> message validators.
	/// </summary>
	[PublicAPI]
	public abstract class ItemAddedValidatorBase : AbstractValidator<ItemAdded>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="ItemAddedValidatorBase"/> type.
		/// </summary>
		protected ItemAddedValidatorBase()
		{
			this.RuleFor(x => x.Metadata)
				.NotEmpty()
				.SetValidator(new ItemAddedDataValidator());
		}
	}
}