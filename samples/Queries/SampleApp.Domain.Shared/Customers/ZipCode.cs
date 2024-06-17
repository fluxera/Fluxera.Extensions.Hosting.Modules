namespace SampleApp.Domain.Shared.Customers
{
	using Fluxera.ValueObject;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class ZipCode : PrimitiveValueObject<ZipCode, string>
	{
		/// <inheritdoc />
		public ZipCode(string value) : base(value)
		{
		}
	}
}
