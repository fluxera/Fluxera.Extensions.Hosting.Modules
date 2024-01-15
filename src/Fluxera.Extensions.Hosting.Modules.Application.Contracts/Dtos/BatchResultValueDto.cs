namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using JetBrains.Annotations;

	///  <summary>
	/// 		An abstract base class for result value containers that hold a correlation token
	///			and the value of a result that belongs to a batch.
	///  </summary>
	///  <typeparam name="TCorrelationToken">The type of the correlation token.</typeparam>
	///  <typeparam name="TValue">The type of the value.</typeparam>
	[PublicAPI]
	[Serializable]
	public abstract class BatchResultValueDto<TCorrelationToken, TValue>
	{
		/// <summary>
		///		Gets or sets the correlation token.
		/// </summary>
		public TCorrelationToken CorrelationToken { get; set; }

		/// <summary>
		///		Gets or sets the value.
		/// </summary>
		public TValue Value { get; set; }

		/// <summary>
		///		Deconstruct the value.
		/// </summary>
		/// <param name="correlationToken"></param>
		/// <param name="value"></param>
		public void Deconstruct(out TCorrelationToken correlationToken, out TValue value)
		{
			correlationToken = this.CorrelationToken;
			value = this.Value;
		}
	}
}
