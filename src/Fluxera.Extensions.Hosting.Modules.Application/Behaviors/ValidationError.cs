﻿namespace Fluxera.Extensions.Hosting.Modules.Application.Behaviors
{
	using JetBrains.Annotations;
	using MadEyeMatt.Results;

	/// <summary>
	///		An error type that adds the property name.
	/// </summary>
	[PublicAPI]
	public sealed class ValidationError : Error
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="ValidationError"/> type.
		/// </summary>
		public ValidationError()
		{
			// Note: Needed for serialization.
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="ValidationError"/> type.
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="errorMessage"></param>
		public ValidationError(string propertyName, string errorMessage)
			: base(errorMessage)
		{
			this.PropertyName = propertyName;
		}

		/// <summary>
		///		Gets the property name.
		/// </summary>
		public string PropertyName { get; }
	}
}