namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
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
				IsSuccessful = true
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
				IsSuccessful = true
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
				IsSuccessful = false,
				Errors =
				{
					ErrorDto.Create(errorMessage, null)
				}
			};
		}

		/// <summary>
		///     Creates a failed result.
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
		///     Creates a failed result.
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static TResultDto Fail<TResultDto>(string errorMessage)
			where TResultDto : ResultBaseDto<ResultDto>, new()
		{
			return new TResultDto
			{
				IsSuccessful = false,
				Errors =
				{
					ErrorDto.Create(errorMessage, null)
				}
			};
		}

		/// <summary>
		///     Creates a failed result.
		/// </summary>
		/// <param name="errorMessages"></param>
		/// <returns></returns>
		public static TResultDto Fail<TResultDto>(IEnumerable<string> errorMessages)
			where TResultDto : ResultBaseDto<ResultDto>, new()
		{
			TResultDto resultDto = new TResultDto
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
				IsSuccessful = true,
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
				IsSuccessful = true,
				Value = value
			};
		}

		/// <summary>
		///     Creates a failed result.
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Fail(string errorMessage)
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
		///     Creates a failed result.
		/// </summary>
		/// <param name="errorMessages"></param>
		/// <returns></returns>
		public static ResultDto<TValue> Fail(IEnumerable<string> errorMessages)
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

		/// <summary>
		///     Creates a failed result.
		/// </summary>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static TResultDto Fail<TResultDto>(string errorMessage)
			where TResultDto : ResultBaseDto<ResultDto<TValue>, TValue>, new()
		{
			return new TResultDto
			{
				IsSuccessful = false,
				Errors =
				{
					ErrorDto.Create(errorMessage, null)
				}
			};
		}

		/// <summary>
		///     Creates a failed result.
		/// </summary>
		/// <param name="errorMessages"></param>
		/// <returns></returns>
		public static TResultDto Fail<TResultDto>(IEnumerable<string> errorMessages)
			where TResultDto : ResultBaseDto<ResultDto<TValue>, TValue>, new()
		{
			TResultDto resultDto = new TResultDto
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
}
