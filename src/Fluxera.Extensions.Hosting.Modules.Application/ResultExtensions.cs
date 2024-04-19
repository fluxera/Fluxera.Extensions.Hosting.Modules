namespace Fluxera.Extensions.Hosting.Modules.Application
{
	using System.Linq;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts;
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
			ResultDto resultDto = new ResultDto
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
			ResultDto<TValue> resultDto = new ResultDto<TValue>
			{
				IsSuccessful = result.IsSuccessful,
				Errors = result.Errors.Select(x => x.ToErrorDto()).ToList(),
				Successes = result.Successes.Select(x => x.ToSuccessDto()).ToList(),
				Value = result.GetValueOrDefault()
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
