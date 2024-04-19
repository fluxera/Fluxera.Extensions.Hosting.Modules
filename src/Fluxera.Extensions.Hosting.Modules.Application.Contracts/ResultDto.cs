namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A DTO for a result.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public class ResultDto : IResultDto
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ResultDto" /> type.
		/// </summary>
		public ResultDto()
		{
			this.Errors = new List<ErrorDto>();
			this.Successes = new List<SuccessDto>();
		}

		/// <inheritdoc />
		public bool IsFailed => !this.IsSuccessful;

		/// <inheritdoc />
		public bool IsSuccessful { get; set; }

		/// <inheritdoc />
		public IList<ErrorDto> Errors { get; set; }

		/// <inheritdoc />
		public IList<SuccessDto> Successes { get; set; }
	}

	/// <summary>
	///     A DTO for a result with a value.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public class ResultDto<TValue> : IResultDto<TValue>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ResultDto{TValue}" /> type.
		/// </summary>
		public ResultDto()
		{
			this.Errors = new List<ErrorDto>();
			this.Successes = new List<SuccessDto>();
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="ResultDto{TValue}" /> type.
		/// </summary>
		/// <param name="value"></param>
		public ResultDto(TValue value) : this()
		{
			this.Value = value;
		}

		/// <inheritdoc />
		public bool IsFailed => !this.IsSuccessful;

		/// <inheritdoc />
		public bool IsSuccessful { get; set; }

		/// <inheritdoc />
		public IList<ErrorDto> Errors { get; set; }

		/// <inheritdoc />
		public IList<SuccessDto> Successes { get; set; }

		/// <summary>
		///     Gets or sets the value of the result.
		/// </summary>
		public TValue Value { get; set; }
	}
}
