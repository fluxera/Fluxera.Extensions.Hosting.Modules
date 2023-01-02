namespace Fluxera.Extensions.Hosting.Modules.MultiTenancy.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class MultiTenancyModuleTests : StartupModuleTestBase<TestModule>
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
		public void ShouldAddTenantContextProvider()
		{
			ITenantContextProvider tenantContextProvider = this.ApplicationLoader.ServiceProvider.GetRequiredService<ITenantContextProvider>();
			tenantContextProvider.Should().NotBeNull();
		}

		[Test]
		public void ShouldConfigureMultiTenancyOptions()
		{
			IOptions<MultiTenancyOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<MultiTenancyOptions>>();
			options.Value.Should().NotBeNull();

			options.Value.Repositories.Should().NotBeNullOrEmpty();
			options.Value.Repositories.Should().HaveCount(1);
			options.Value.Repositories.Keys.Should().Contain("Test");
			options.Value.Repositories["Test"].Enabled.Should().BeTrue();
			options.Value.Repositories["Test"].Mode.Should().Be(MultiTenancyMode.SingleDatabase);
		}
	}
}
