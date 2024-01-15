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
	public sealed class BatchResultDto<TResult> where TResult : ResultBaseDto<TResult>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="BatchResultDto{TValue}"/> type.
		/// </summary>
		public BatchResultDto()
		{
			// Note: Needed for serialization.
		}

		private BatchResultDto(IEnumerable<TResult> results)
		{
			results ??= Enumerable.Empty<TResult>();

			this.Results = new List<TResult>(results);
		}

		/// <summary>
		///		Flag, indicating if one of the batched result was successful.
		/// </summary>
		public bool IsSuccess => !this.IsFailed;

		/// <summary>
		///		Flag, indicating if one of the batched results was failed.
		/// </summary>
		public bool IsFailed => this.Results.Any(x => x.IsFailed);

		/// <summary>
		///		Gets or sets the results.
		/// </summary>
		public IList<TResult> Results { get; set; }

		/// <summary>
		///		Creates a new batched result for the given results.
		/// </summary>
		/// <param name="results"></param>
		/// <returns></returns>
		public BatchResultDto<TResult> Create(IEnumerable<TResult> results)
		{
			return new BatchResultDto<TResult>(results);
		}
	}

	/// <summary>
	///		A DTO for batched operation that may return multiple results with a value.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class BatchResultDto<TResult, TValue> where TResult : ResultBaseDto<TResult, TValue>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="BatchResultDto{TValue}"/> type.
		/// </summary>
		public BatchResultDto()
		{
			// Note: Needed for serialization.
		}

		private BatchResultDto(IEnumerable<TResult> results)
		{
			results ??= Enumerable.Empty<TResult>();

			this.Results = new List<TResult>(results);
		}

		/// <summary>
		///		Flag, indicating if one of the batched result was successful.
		/// </summary>
		public bool IsSuccess => !this.IsFailed;

		/// <summary>
		///		Flag, indicating if one of the batched results was failed.
		/// </summary>
		public bool IsFailed => this.Results.Any(x => x.IsFailed);

		/// <summary>
		///		Gets or sets the results.
		/// </summary>
		public IList<TResult> Results { get; set; }

		/// <summary>
		///		Creates a new batched result for the given results.
		/// </summary>
		/// <param name="results"></param>
		/// <returns></returns>
		public BatchResultDto<TResult, TValue> Create(IEnumerable<TResult> results)
		{
			return new BatchResultDto<TResult, TValue>(results);
		}
	}
}
