namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for sending integration commands.
	/// </summary>
	[PublicAPI]
	public interface IIntegrationSender
	{
		/// <summary>
		///		Send the given integration command.
		/// </summary>
		/// <typeparam name="TCommand"></typeparam>
		/// <param name="command"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
			where TCommand : class, IIntegrationCommand;
	}
}