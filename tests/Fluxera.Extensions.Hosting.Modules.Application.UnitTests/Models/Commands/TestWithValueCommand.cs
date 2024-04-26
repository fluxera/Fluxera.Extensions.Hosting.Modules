namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Commands
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;

	public class TestWithValueCommand : IApplicationCommand<int>
	{
		public string Name { get; set; }
	}
}
