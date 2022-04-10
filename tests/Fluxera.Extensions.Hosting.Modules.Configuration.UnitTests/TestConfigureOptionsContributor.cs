namespace Fluxera.Extensions.Hosting.Modules.Configuration.UnitTests
{
	public class TestConfigureOptionsContributor : ConfigureOptionsContributorBase<TestOptions>
	{
		/// <inheritdoc />
		public override string SectionName => "Test";
	}
}
