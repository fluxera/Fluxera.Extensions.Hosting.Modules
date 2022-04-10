namespace Fluxera.Extensions.Hosting.Modules.AutoMapper.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using NUnit.Framework;

	[TestFixture]
	public class AutoMapperModuleTests : StartupModuleTestBase<AutoMapperModule>
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
		public void ShouldConfigureAutoMapperOptions()
		{
			IOptions<AutoMapperOptions> options = this.ApplicationLoader.ServiceProvider.GetRequiredService<IOptions<AutoMapperOptions>>();
			options.Value.Should().NotBeNull();
		}
	}
}
