namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for a success dto.
	/// </summary>
	[PublicAPI]
	public interface ISuccessDto
	{
		/// <summary>
		///		Gets or sets the message.
		/// </summary>
		string Message { get; set; }

		/// <summary>
		///		Gets or sets the success metadata.
		/// </summary>
		IDictionary<string, object> Metadata { get; set; }
	}
}
