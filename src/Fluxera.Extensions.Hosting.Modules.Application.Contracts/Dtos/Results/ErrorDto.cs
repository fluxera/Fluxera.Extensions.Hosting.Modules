﻿namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos.Results
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///		A DTO for error messages of a <see cref="ResultDto"/>.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public class ErrorDto : IErrorDto
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="ErrorDto"/> type.
		/// </summary>
		public ErrorDto()
		{
			// Note: Needed for serialization.
		}

		private ErrorDto(string message, IDictionary<string, object> metadata)
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
		///		Creates a new instance of the error.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="metadata"></param>
		/// <returns></returns>
		public static ErrorDto Create(string message, IDictionary<string, object> metadata)
		{
			return new ErrorDto(message, metadata);
		}
	}
}
