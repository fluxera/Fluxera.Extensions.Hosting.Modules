namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using JetBrains.Annotations;

	///  <summary>
	/// 		A result value container that holds a correlation token and the value of a result that belongs to a batch.
	///  </summary>
	///  <typeparam name="TCorrelationToken">The type of the correlation token.</typeparam>
	///  <typeparam name="TValue">The type of the value.</typeparam>
	[PublicAPI]
	[Serializable]
	public sealed class BatchResultValueDto<TCorrelationToken, TValue>
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="BatchResultValueDto{TCorrelationToken, TValue}"/> type.
		/// </summary>
		public BatchResultValueDto()
		{
			// Note: Needed for serialization.
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="BatchResultValueDto{TCorrelationToken, TValue}"/> type.
		/// </summary>
		public BatchResultValueDto(TCorrelationToken correlationToken, TValue value)
		{
			this.CorrelationToken = correlationToken;
			this.Value = value;
		}

		/// <summary>
		///		Gets or sets the correlation token.
		/// </summary>
		public TCorrelationToken CorrelationToken { get; set; }

		/// <summary>
		///		Gets or sets the value.
		/// </summary>
		public TValue Value { get; set; }
	}
}
