namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.Authentication.UnitTests
{
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class AuthenticationSchemeExtensionsTests
	{
		[Test]
		public void ShouldCreateSchemeName()
		{
			const string key = "Second";
			string schemeName = key.CalculateSchemeName("Bearer");

			schemeName.Should().NotBeNullOrEmpty();
			schemeName.Should().Be("Bearer-Second");
		}

		[Test]
		public void ShouldCreateSchemeName_ForDefaultAuthScheme()
		{
			const string key = "Bearer";
			string schemeName = key.CalculateSchemeName("Bearer");

			schemeName.Should().NotBeNullOrEmpty();
			schemeName.Should().Be("Bearer");
		}

		[Test]
		public void ShouldCreateSchemeName_ForDefaultKey()
		{
			const string key = "Default";
			string schemeName = key.CalculateSchemeName("Bearer");

			schemeName.Should().NotBeNullOrEmpty();
			schemeName.Should().Be("Bearer");
		}
	}
}
