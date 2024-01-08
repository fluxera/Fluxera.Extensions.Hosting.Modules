namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     A list result implementation.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[PublicAPI]
	[Serializable]
	public class ListResultDto<T> : IListResult<T>
	{
		private IReadOnlyList<T> items;

		/// <summary>
		///     Creates a new <see cref="ListResultDto{T}" /> object.
		/// </summary>
		public ListResultDto()
		{
			// Note: Needed for serialization.
		}

		/// <summary>
		///     Creates a new <see cref="ListResultDto{T}" /> object.
		/// </summary>
		/// <param name="items">List of items</param>
		public ListResultDto(IReadOnlyList<T> items)
		{
			this.Items = items;
		}

		/// <inheritdoc />
		public IReadOnlyList<T> Items
		{
			get => this.items ??= new List<T>();
			set => this.items = value;
		}
	}
}
