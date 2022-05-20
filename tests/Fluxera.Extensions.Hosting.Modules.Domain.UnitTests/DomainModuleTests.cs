namespace Fluxera.Extensions.Hosting.Modules.Domain.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class DomainModuleTests : StartupModuleTestBase<TestModule>
	{
		[SetUp]
		public void SetUp()
		{
			this.StartApplication();
		}

		[TearDown]
		public void TearDown()
		{
			this.StopApplication();
		}

		[Test]
		public void ShouldAddDateTimeOffsetProvider()
		{
			IDateTimeOffsetProvider dateTimeOffsetProvider = this.ApplicationLoader.ServiceProvider.GetRequiredService<IDateTimeOffsetProvider>();
			dateTimeOffsetProvider.Should().NotBeNull();
		}
	}
}
