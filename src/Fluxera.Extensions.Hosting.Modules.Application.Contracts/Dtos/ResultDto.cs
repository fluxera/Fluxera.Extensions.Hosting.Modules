namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using JetBrains.Annotations;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	///		A DTO for a result.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ResultDto
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="ResultDto"/> type.
		/// </summary>
		public ResultDto()
		{
			// Note: Needed for serialization.
		}

		private ResultDto(bool isSuccess, IEnumerable<ErrorDto> errors = null, IEnumerable<SuccessDto> successes = null)
		{
			this.IsSuccess = isSuccess;
			this.IsFailed = !isSuccess;
			this.Errors = new List<ErrorDto>(errors ?? Enumerable.Empty<ErrorDto>());
			this.Successes = new List<SuccessDto>(successes ?? Enumerable.Empty<SuccessDto>());
		}

		/// <summary>
		///		Flag, indicating if the result was successful.
		/// </summary>
		public bool IsSuccess { get; set; }

		/// <summary>
		///		Flag, indicating if the result was failed.
		/// </summary>
		public bool IsFailed { get; set; }

		/// <summary>
		///		Gets the potential errors.
		/// </summary>
		public IList<ErrorDto> Errors { get; set; }

		/// <summary>
		///		Gets the potential successes.
		/// </summary>
		public IList<SuccessDto> Successes { get; set; }

		/// <summary>
		///		Creates a new successful instance of the result.
		/// </summary>
		/// <param name="successes"></param>
		/// <returns></returns>
		public static ResultDto CreateSuccess(IEnumerable<SuccessDto> successes = null)
		{
			return new ResultDto(true, successes: successes);
		}

		/// <summary>
		///		Creates a new failed instance of the result.
		/// </summary>
		/// <param name="errors"></param>
		/// <returns></returns>
		public static ResultDto CreateFailure(IEnumerable<ErrorDto> errors = null)
		{
			return new ResultDto(true, errors: errors);
		}

		/// <summary>
		///		Creates a successful result.
		/// </summary>
		/// <returns></returns>
		public static ResultDto Ok()
		{
			return new ResultDto(true);
		}

		/// <summary>
		///		Creates a failed result.
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static ResultDto Fail(string errorMessage)
		{
			return new ResultDto(false, new List<ErrorDto>
			{
				ErrorDto.Create(errorMessage, null)
			});
		}
	}

	/// <summary>
	///		A DTO for a result with a value.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ResultDto<TValue> 
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="ResultDto{TValue}"/> type.
		/// </summary>
		public ResultDto()
		{
			// Note: Needed for serialization.
		}

		private ResultDto(bool isSuccess, TValue value, IEnumerable<ErrorDto> errors = null, IEnumerable<SuccessDto> successes = null)
		{
			this.IsSuccess = isSuccess;
			this.IsFailed = !isSuccess;
			this.Value = value;
			this.Errors = new List<ErrorDto>(errors ?? Enumerable.Empty<ErrorDto>());
			this.Successes = new List<SuccessDto>(successes ?? Enumerable.Empty<SuccessDto>());
		}

		/// <summary>
		///		Flag, indicating if the result was successful.
		/// </summary>
		public bool IsSuccess { get; set; }

		/// <summary>
		///		Flag, indicating if the result was failed.
		/// </summary>
		public bool IsFailed { get; set; }

		/// <summary>
		///		Gets the potential errors.
		/// </summary>
		public IList<ErrorDto> Errors { get; set; }

		/// <summary>
		///		Gets the potential successes.
		/// </summary>
		public IList<SuccessDto> Successes { get; set; }

		/// <summary>
		///		Gets or sets the value of the result.
		/// </summary>
		public TValue Value { get; set; }

		/// <summary>
		///		Creates a new successful instance of the result.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="successes"></param>
		/// <returns></returns>
		public static ResultDto<TValue> CreateSuccess(TValue value, IEnumerable<SuccessDto> successes = null)
		{
			return new ResultDto<TValue>(true, value, successes: successes);
		}

		/// <summary>
		///		Creates a new failed instance of the result.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="errors"></param>
		/// <returns></returns>
		public static ResultDto<TValue> CreateFailure(TValue value, IEnumerable<ErrorDto> errors = null)
		{
			return new ResultDto<TValue>(false, value, errors: errors);
		}

		/// <summary>
		///		Creates a successful result.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Ok(TValue value)
		{
			return new ResultDto<TValue>(true, value);
		}

		///  <summary>
		/// 		Creates a failed result.
		///  </summary>
		///  <param name="value"></param>
		///  <param name="errorMessage"></param>
		///  <returns></returns>
		public static ResultDto<TValue> Fail(TValue value, string errorMessage)
		{
			return new ResultDto<TValue>(false, value, new List<ErrorDto>
			{
				ErrorDto.Create(errorMessage, null)
			});
		}
	}
}
