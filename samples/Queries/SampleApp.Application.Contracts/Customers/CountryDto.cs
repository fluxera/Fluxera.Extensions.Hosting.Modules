namespace SampleApp.Application.Contracts.Customers
{
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class CountryDto
	{
		public string Code { get; set; }

		public string Name { get; set; }
	}
}
