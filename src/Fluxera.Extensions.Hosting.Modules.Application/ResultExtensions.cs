namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using System.Linq;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="Result" /> type.
	/// </summary>
	[PublicAPI]
	public static class ResultExtensions
	{
		/// <summary>
		///     Converts the given result to a result DTO.
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public static ResultDto ToResultDto(this Result result)
		{
			return result.ToResultDto<ResultDto>();
		}

		/// <summary>
		///     Converts the given result to a result DTO.
		/// </summary>
		/// <param name="result"></param>
		/// <returns></returns>
		public static TResultDto ToResultDto<TResultDto>(this Result result) where TResultDto : ResultDto, new()
		{
			TResultDto resultDto = new TResultDto
			{
				IsSuccess = result.IsSuccess,
				Errors = result.Errors.Select(x => x.ToErrorDto()).ToList(),
				Successes = result.Successes.Select(x => x.ToSuccessDto()).ToList()
			};

			return resultDto;
		}

		/// <summary>
		///     Converts the given result to a result DTO.
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="result"></param>
		/// <returns></returns>
		public static ResultDto<TValue> ToResultDto<TValue>(this Result<TValue> result)
		{
			return result.ToResultDto<ResultDto<TValue>, TValue>();
		}

		/// <summary>
		///     Converts the given result to a result DTO.
		/// </summary>
		/// <typeparam name="TResultDto"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="result"></param>
		/// <returns></returns>
		public static TResultDto ToResultDto<TResultDto, TValue>(this Result<TValue> result) where TResultDto : ResultDto<TValue>, new()
		{
			TResultDto resultDto = new TResultDto
			{
				IsSuccess = result.IsSuccess,
				Errors = result.Errors.Select(x => x.ToErrorDto()).ToList(),
				Successes = result.Successes.Select(x => x.ToSuccessDto()).ToList(),
				Value = result.ValueOrDefault
			};

			return resultDto;
		}

		private static ErrorDto ToErrorDto(this IError error)
		{
			return ErrorDto.Create(error.Message, error.Metadata);
		}

		private static SuccessDto ToSuccessDto(this ISuccess success)
		{
			return SuccessDto.Create(success.Message, success.Metadata);
		}
	}
}
