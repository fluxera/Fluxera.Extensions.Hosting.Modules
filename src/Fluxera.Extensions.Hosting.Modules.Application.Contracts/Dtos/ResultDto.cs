namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A DTO for a result.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ResultDto : ResultBaseDto<ResultDto>
	{
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
		public static TResultDto Ok<TResultDto>()
			where TResultDto : ResultBaseDto<ResultDto>, new()
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
		public static TResultDto Fail<TResultDto>(string errorMessage)
			where TResultDto : ResultBaseDto<ResultDto>, new()
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
	public sealed class ResultDto<TValue> : ResultBaseDto<ResultDto<TValue>, TValue>
	{
		/// <summary>
		///     Creates a successful result.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Ok(TValue value = default)
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
		public static TResultDto Ok<TResultDto>(TValue value = default)
			where TResultDto : ResultBaseDto<ResultDto<TValue>, TValue>, new()
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
		/// <param name="errorMessage"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Fail(string errorMessage, TValue value = default)
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
		/// <param name="errorMessage"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static TResultDto Fail<TResultDto>(string errorMessage, TValue value = default)
			where TResultDto : ResultBaseDto<ResultDto<TValue>, TValue>, new()
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
