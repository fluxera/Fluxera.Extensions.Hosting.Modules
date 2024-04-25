namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos.Results
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for result types without value.
	/// </summary>
	[PublicAPI]
	public interface IResultDto
	{
		/// <summary>
		///		Flag, indicating if the result was failed.
		/// </summary>
		bool IsFailed { get; }

		/// <summary>
		///		Flag, indicating if the result was successful.
		/// </summary>
		bool IsSuccessful { get; }

		/// <summary>
		///		Gets the potential errors.
		/// </summary>
		IList<ErrorDto> Errors { get; }

		/// <summary>
		///		Gets the potential successes.
		/// </summary>
		IList<SuccessDto> Successes { get; }
	}

	/// <summary>
	///		A contract for result types with value.
	/// </summary>
	[PublicAPI]
	public interface IResultDto<TValue> : IResultDto
	{
		/// <summary>
		///     Gets or sets the value of the result.
		/// </summary>
		TValue Value { get; set; }
	}
}
