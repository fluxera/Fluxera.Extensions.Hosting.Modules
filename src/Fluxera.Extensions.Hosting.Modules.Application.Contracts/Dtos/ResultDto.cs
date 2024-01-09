namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A DTO for a result.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public class ResultDto
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ResultDto" /> type.
		/// </summary>
		public ResultDto()
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
		///     Creates a successful result.
		/// </summary>
		/// <returns></returns>
		public static ResultDto Ok()
		{
			return new ResultDto
			{
				IsSuccess = true
			};
		}

		/// <summary>
		///     Creates a successful result.
		/// </summary>
		/// <returns></returns>
		public static TResultDto Ok<TResultDto>() where TResultDto : ResultDto, new()
		{
			return new TResultDto
			{
				IsSuccess = true
			};
		}

		/// <summary>
		///     Creates a failed result.
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static ResultDto Fail(string errorMessage)
		{
			return new ResultDto
			{
				IsSuccess = false,
				Errors =
				{
					ErrorDto.Create(errorMessage, null)
				}
			};
		}

		/// <summary>
		///     Creates a failed result.
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static TResultDto Fail<TResultDto>(string errorMessage) where TResultDto : ResultDto, new()
		{
			return new TResultDto
			{
				IsSuccess = false,
				Errors =
				{
					ErrorDto.Create(errorMessage, null)
				}
			};
		}
	}

	/// <summary>
	///     A DTO for a result with a value.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public class ResultDto<TValue> : ResultDto
	{
		/// <summary>
		///     Gets or sets the value of the result.
		/// </summary>
		public TValue Value { get; set; }

		/// <summary>
		///     Creates a successful result.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Ok(TValue value)
		{
			return new ResultDto<TValue>
			{
				IsSuccess = true,
				Value = value
			};
		}

		/// <summary>
		///     Creates a successful result.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static TResultDto Ok<TResultDto>(TValue value) where TResultDto : ResultDto<TValue>, new()
		{
			return new TResultDto
			{
				IsSuccess = true,
				Value = value
			};
		}

		/// <summary>
		///     Creates a failed result.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Fail(TValue value, string errorMessage)
		{
			return new ResultDto<TValue>
			{
				IsSuccess = false,
				Value = value,
				Errors =
				{
					ErrorDto.Create(errorMessage, null)
				}
			};
		}

		/// <summary>
		///     Creates a failed result.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static TResultDto Fail<TResultDto>(TValue value, string errorMessage) where TResultDto : ResultDto<TValue>, new()
		{
			return new TResultDto
			{
				IsSuccess = false,
				Value = value,
				Errors =
				{
					ErrorDto.Create(errorMessage, null)
				}
			};
		}
	}
}
