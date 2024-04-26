namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Events
{
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;

	public class TestEvent : IApplicationEvent
	{
		public string Name { get; set; }
	}
}
