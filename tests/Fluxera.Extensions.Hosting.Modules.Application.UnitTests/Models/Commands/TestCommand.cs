namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Commands
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;

	public class TestCommand : IApplicationCommand
	{
		public string Name { get; set; }
	}
}
