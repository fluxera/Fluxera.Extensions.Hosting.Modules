namespace Fluxera.Extensions.Hosting.Modules.Messaging.Filters
{
	using System.Threading.Tasks;
	using MassTransit;

	/// <summary>
	///     A contract for a message authenticator.
	/// </summary>
	public interface IMessageAuthenticator
	{
		/// <summary>
		///     Authenticates the given message context by adding the available
		///     access token to the headers of the message. Clients may get the
		///     principal with it later on.
		/// </summary>
		/// <typeparam name="T">The message type.</typeparam>
		/// <param name="context">The context to authenticate.</param>
		Task AuthenticateMessage<T>(SendContext<T> context) where T : class;
	}
}
