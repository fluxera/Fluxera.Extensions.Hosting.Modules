namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models.Events
{
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Handlers;

	public class TestEventHandler : IApplicationEventHandler<TestEvent>
	{
		/// <inheritdoc />
		public Task HandleAsync(TestEvent notification, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
