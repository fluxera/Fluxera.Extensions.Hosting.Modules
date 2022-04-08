namespace Fluxera.Extensions.Hosting.Modules.DataManagement.UnitTests
{
	using Fluxera.Extensions.Hosting.Modules.UnitTesting;
	using NUnit.Framework;

	[TestFixture]
	public class DataManagementModuleTests : StartupModuleTestBase<DataManagementModule>
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
		public void Should()
		{
		}
	}
}
