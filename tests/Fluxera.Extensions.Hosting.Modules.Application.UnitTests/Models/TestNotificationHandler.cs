namespace Fluxera.Extensions.Hosting.Modules.Application.UnitTests.Models
{
	using System.Threading;
	using System.Threading.Tasks;
	using MediatR;

	public class TestNotificationHandler : INotificationHandler<TestNotification>
	{
		/// <inheritdoc />
		public Task Handle(TestNotification notification, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
