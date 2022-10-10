namespace Fluxera.Extensions.Hosting.Modules.Configuration.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using NUnit.Framework;

	[TestFixture]
	public class ConfigurationModuleTests : StartupModuleTestBase<ConfigurationModule>
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
	}
}
