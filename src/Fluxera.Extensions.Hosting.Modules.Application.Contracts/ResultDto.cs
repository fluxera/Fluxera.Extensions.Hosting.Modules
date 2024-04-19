namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
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

		/// <summary>
		///     Creates a successful result DTO.
		/// </summary>
		/// <returns></returns>
		public static ResultDto Ok()
		{
			return new ResultDto
			{
				IsSuccessful = true
			};
		}

		/// <summary>
		///     Creates a successful result DTO.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Ok<TValue>(TValue value = default)
		{
			return new ResultDto<TValue>
			{
				IsSuccessful = true,
				Value = value
			};
		}

		/// <summary>
		///     Creates a failed result DTO.
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static ResultDto Fail(string errorMessage)
		{
			return new ResultDto
			{
				IsSuccessful = false,
				Errors =
				{
					ErrorDto.Create(errorMessage, null)
				}
			};
		}

		/// <summary>
		///     Creates a failed result DTO.
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Fail<TValue>(string errorMessage)
		{
			return new ResultDto<TValue>
			{
				IsSuccessful = false,
				Errors =
				{
					ErrorDto.Create(errorMessage, null)
				}
			};
		}

		/// <summary>
		///     Creates a failed result DTO.
		/// </summary>
		/// <param name="errorMessages"></param>
		/// <returns></returns>
		public static ResultDto Fail(IEnumerable<string> errorMessages)
		{
			ResultDto resultDto = new ResultDto
			{
				IsSuccessful = false
			};

			foreach(string errorMessage in errorMessages ?? Enumerable.Empty<string>())
			{
				resultDto.Errors.Add(ErrorDto.Create(errorMessage, null));
			}

			return resultDto;
		}

		/// <summary>
		///     Creates a failed result DTO.
		/// </summary>
		/// <param name="errorMessages"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Fail<TValue>(IEnumerable<string> errorMessages)
		{
			ResultDto<TValue> resultDto = new ResultDto<TValue>
			{
				IsSuccessful = false
			};

			foreach(string errorMessage in errorMessages ?? Enumerable.Empty<string>())
			{
				resultDto.Errors.Add(ErrorDto.Create(errorMessage, null));
			}

			return resultDto;
		}
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
