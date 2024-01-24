namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///		A DTO for batched operation that may return multiple results with a value.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class BatchResultDto<TResult> : IResultDto
		where TResult : class, IResultDto
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="BatchResultDto{TResult}"/> type.
		/// </summary>
		public BatchResultDto() : this(Enumerable.Empty<TResult>())
		{
			// Note: Needed for serialization.
		}

		private BatchResultDto(IEnumerable<TResult> results)
		{
			results ??= Enumerable.Empty<TResult>();

			this.Results = new List<TResult>(results);
		}

		/// <summary>
		///		Flag, indicating if one of the batched results was failed.
		/// </summary>
		public bool IsFailed => this.Results.Any(x => x.IsFailed);

		/// <summary>
		///		Flag, indicating if one of the batched result was successful.
		/// </summary>
		public bool IsSuccessful => !this.IsFailed;

		/// <inheritdoc />
		public IList<ErrorDto> Errors => this.Results.SelectMany(x => x.Errors).ToList();

		/// <inheritdoc />
		public IList<SuccessDto> Successes => this.Results.SelectMany(x => x.Successes).ToList();

		/// <summary>
		///		Gets the results.
		/// </summary>
		public IList<TResult> Results { get; private set; }

		/// <summary>
		///		Creates a new batched result for the given results.
		/// </summary>
		/// <param name="results"></param>
		/// <returns></returns>
		public static BatchResultDto<TResult> Create(IEnumerable<TResult> results)
		{
			return new BatchResultDto<TResult>(results);
		}
	}
}
