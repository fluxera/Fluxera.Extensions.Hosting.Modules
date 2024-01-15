namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     An abstract base class for DTOs for results.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public abstract class ResultBaseDto<TResult> 
		where TResult : ResultBaseDto<TResult>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ResultBaseDto{TResult}" /> type.
		/// </summary>
		protected ResultBaseDto()
		{
			// Note: Needed for serialization.

			this.Errors = new List<ErrorDto>();
			this.Successes = new List<SuccessDto>();
		}

		/// <summary>
		///     Flag, indicating if the result was successful.
		/// </summary>
		public bool IsSuccess { get; set; }

		/// <summary>
		///     Flag, indicating if the result was failed.
		/// </summary>
		public bool IsFailed => !this.IsSuccess;

		/// <summary>
		///     Gets the potential errors.
		/// </summary>
		public IList<ErrorDto> Errors { get; set; }

		/// <summary>
		///     Gets the potential successes.
		/// </summary>
		public IList<SuccessDto> Successes { get; set; }
	}

	/// <summary>
	///     An abstract base class for DTOs for results.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public abstract class ResultBaseDto<TResult, TValue> 
		where TResult : ResultBaseDto<TResult, TValue>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ResultBaseDto{TResult, TValue}" /> type.
		/// </summary>
		protected ResultBaseDto()
		{
			// Note: Needed for serialization.

			this.Errors = new List<ErrorDto>();
			this.Successes = new List<SuccessDto>();
		}

		/// <summary>
		///     Flag, indicating if the result was successful.
		/// </summary>
		public bool IsSuccess { get; set; }

		/// <summary>
		///     Flag, indicating if the result was failed.
		/// </summary>
		public bool IsFailed => !this.IsSuccess;

		/// <summary>
		///     Gets the potential errors.
		/// </summary>
		public IList<ErrorDto> Errors { get; set; }

		/// <summary>
		///     Gets the potential successes.
		/// </summary>
		public IList<SuccessDto> Successes { get; set; }

		/// <summary>
		///     Gets or sets the value of the result.
		/// </summary>
		public TValue Value { get; set; }
	}
}
