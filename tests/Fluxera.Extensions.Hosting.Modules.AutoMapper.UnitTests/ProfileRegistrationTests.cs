namespace Fluxera.Extensions.Hosting.Modules.AutoMapper.UnitTests
{
	using FluentAssertions;
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using global::AutoMapper;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ProfileRegistrationTests : StartupModuleTestBase<TestModule>
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
		public void ShouldConfigureMapper()
		{
			IMapper mapper = this.ApplicationLoader.ServiceProvider.GetRequiredService<IMapper>();
			mapper.Should().NotBeNull();

			Test2 instance = mapper.Map<Test2>(new Test1 { String = "Hello" });
			instance.Should().NotBeNull();
			instance.String.Should().Be("Hello");
		}
	}
}
