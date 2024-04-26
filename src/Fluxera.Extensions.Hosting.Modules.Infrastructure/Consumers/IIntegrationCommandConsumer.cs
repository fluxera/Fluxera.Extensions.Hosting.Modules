namespace Fluxera.Extensions.Hosting.Modules.Infrastructure.Consumers
{
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
	using JetBrains.Annotations;
	using MassTransit;

	/// <summary>
	///		Defines a class that is a consumer of an integration command message.
	/// </summary>
	/// <typeparam name="TCommand">The command message type.</typeparam>
	[PublicAPI]
	public interface IIntegrationCommandConsumer<in TCommand> : IConsumer<TCommand>
		where TCommand : class, IIntegrationCommand
	{
		/// <summary>
		///		Consumes the command message.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		Task ConsumeAsync(ConsumeContext<TCommand> context);

		/// <inheritdoc />
		Task IConsumer<TCommand>.Consume(ConsumeContext<TCommand> context)
		{
			return this.ConsumeAsync(context);
		}
	}
}
