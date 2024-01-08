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
	public sealed class BatchResultDto<TValue>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="BatchResultDto{TValue}"/> type.
		/// </summary>
		public BatchResultDto()
		{
			// Note: Needed for serialization.
		}

		private BatchResultDto(IEnumerable<ResultDto<TValue>> results)
		{
			results ??= Enumerable.Empty<ResultDto<TValue>>();

			this.Results = new List<ResultDto<TValue>>(results);
		}

		/// <summary>
		///		Flag, indicating if one of the batched result was successful.
		/// </summary>
		public bool IsSuccess => this.Results.All(x => x.IsSuccess);

		/// <summary>
		///		Flag, indicating if one of the batched results was failed.
		/// </summary>
		public bool IsFailed => this.Results.All(x => x.IsFailed);

		/// <summary>
		///		Gets or sets the results.
		/// </summary>
		public IList<ResultDto<TValue>> Results { get; set; }

		/// <summary>
		///		Creates a new batched result for the given results.
		/// </summary>
		/// <param name="results"></param>
		/// <returns></returns>
		public BatchResultDto<TValue> Create(IEnumerable<ResultDto<TValue>> results)
		{
			return new BatchResultDto<TValue>(results);
		}
	}
}
