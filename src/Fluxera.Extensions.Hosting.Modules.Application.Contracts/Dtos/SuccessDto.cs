namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;



	/// <summary>
	///		A DTO for success messages of a <see cref="ResultDto"/>.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class SuccessDto : ISuccessDto
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="SuccessDto"/> type.
		/// </summary>
		public SuccessDto()
		{
			// Note: Needed for serialization.
		}

		private SuccessDto(string message, IDictionary<string, object> metadata)
		{
			metadata ??= new Dictionary<string, object>();

			this.Message = message;
			this.Metadata = new Dictionary<string, object>(metadata);
		}

		/// <summary>
		///		Gets or sets the message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		///		Gets or sets the success metadata.
		/// </summary>
		public IDictionary<string, object> Metadata { get; set; }

		/// <summary>
		///		Creates a new instance of the success.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="metadata"></param>
		/// <returns></returns>
		public static SuccessDto Create(string message, IDictionary<string, object> metadata)
		{
			return new SuccessDto(message, metadata);
		}
	}
}
