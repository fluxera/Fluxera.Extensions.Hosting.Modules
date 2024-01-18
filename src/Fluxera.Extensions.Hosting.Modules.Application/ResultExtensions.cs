namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using System.Linq;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;
	using MadEyeMatt.Results;

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
		public static TResultDto ToResultDto<TResultDto>(this IResult result) 
			where TResultDto : ResultBaseDto<TResultDto>, new()
		{
			TResultDto resultDto = new TResultDto
			{
				IsSuccessful = result.IsSuccessful,
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
		public static ResultDto<TValue> ToResultDto<TValue>(this IResult<TValue> result)
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
		public static TResultDto ToResultDto<TResultDto, TValue>(this IResult<TValue> result) 
			where TResultDto : ResultBaseDto<TResultDto, TValue>, new()
		{
			TResultDto resultDto = new TResultDto
			{
				IsSuccessful = result.IsSuccessful,
				Errors = result.Errors.Select(x => x.ToErrorDto()).ToList(),
				Successes = result.Successes.Select(x => x.ToSuccessDto()).ToList(),
				Value = result.GetValueOrDefault()
			};

			return resultDto;
		}

		/// <summary>
		///		Converts the given batch result to a batch result DTO.
		/// </summary>
		/// <typeparam name="TResultDto"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="batchResult"></param>
		/// <returns></returns>
		public static BatchResultDto<TResultDto> ToResultDto<TResultDto, TResult>(this BatchResult<TResult> batchResult)
			where TResultDto : ResultBaseDto<TResultDto>, new()
			where TResult : ResultBase<TResult>
		{
			BatchResultDto<TResultDto> batchResultDto = new BatchResultDto<TResultDto>();

			foreach(TResult result in batchResult.Results)
			{
				TResultDto resultDto = new TResultDto
				{
					IsSuccessful = result.IsSuccessful,
					Errors = result.Errors.Select(x => x.ToErrorDto()).ToList(),
					Successes = result.Successes.Select(x => x.ToSuccessDto()).ToList(),
				};

				batchResultDto.Results.Add(resultDto);
			}

			return batchResultDto;
		}

		///  <summary>
		/// 		Converts the given batch result to a batch result DTO.
		///  </summary>
		///  <typeparam name="TResultDto"></typeparam>
		///  <typeparam name="TResult"></typeparam>
		///  <typeparam name="TValue"></typeparam>
		///  <param name="batchResult"></param>
		///  <returns></returns>
		public static BatchResultDto<TResultDto> ToResultDto<TResultDto, TResult, TValue>(this BatchResult<TResult, TValue> batchResult)
			where TResultDto : ResultBaseDto<TResultDto, TValue>, new()
			where TResult : ResultBase<TResult, TValue>
		{
			BatchResultDto<TResultDto> batchResultDto = new BatchResultDto<TResultDto>();

			foreach(TResult result in batchResult.Results)
			{
				TResultDto resultDto = new TResultDto
				{
					IsSuccessful = result.IsSuccessful,
					Errors = result.Errors.Select(x => x.ToErrorDto()).ToList(),
					Successes = result.Successes.Select(x => x.ToSuccessDto()).ToList(),
					Value = result.GetValueOrDefault()
				};

				batchResultDto.Results.Add(resultDto);
			}

			return batchResultDto;
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
