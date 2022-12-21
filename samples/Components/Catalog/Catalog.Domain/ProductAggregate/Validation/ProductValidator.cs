namespace Catalog.Domain.ProductAggregate.Validation
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
				.MaximumLength(256);

			this.RuleFor(x => x.Description)
				.NotEmpty()
				.MaximumLength(1024);
		}
	}
}
