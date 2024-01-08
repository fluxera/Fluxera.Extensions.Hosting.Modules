namespace Catalog.Application
{
	using System.Linq;
	using FluentResults;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using JetBrains.Annotations;

	/// <summary>
	///		Extension methods for the <see cref="Result"/> type.
	/// </summary>
	[PublicAPI]
	public static class ResultExtensions
	{
		public static ResultDto ToResultDto(this Result result)
		{
			return result.IsSuccess
				? ResultDto.CreateSuccess(result.Successes.Select(x => x.ToSuccessDto()))
				: ResultDto.CreateFailure(result.Errors.Select(x => x.ToErrorDto()));
		}

		public static ResultDto<TValue> ToResultDto<TValue>(this Result<TValue> result)
		{
			return result.IsSuccess
				? ResultDto<TValue>.CreateSuccess(result.ValueOrDefault, result.Successes.Select(x => x.ToSuccessDto()))
				: ResultDto<TValue>.CreateFailure(result.ValueOrDefault, result.Errors.Select(x => x.ToErrorDto()));
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
