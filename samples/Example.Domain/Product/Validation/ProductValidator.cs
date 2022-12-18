namespace Catalog.Domain.Product.Validation
{
	using FluentValidation;
	using JetBrains.Annotations;

	/// <summary>
	///     A validator that validates example instances.
	/// </summary>
	[UsedImplicitly]
	public sealed class ProductValidator : AbstractValidator<Product>
	{
		public ProductValidator()
		{
			this.RuleFor(x => x.Name)
				.NotEmpty()
				.MaximumLength(100);
		}
	}
}
