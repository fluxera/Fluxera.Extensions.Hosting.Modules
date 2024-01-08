namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     Implements <see cref="IPagedResult{T}" />.
	/// </summary>
	/// <typeparam name="T">Type of the items in the <see cref="ListResultDto{T}.Items" /> list</typeparam>
	[PublicAPI]
	[Serializable]
	public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>
	{
		/// <summary>
		///     Creates a new <see cref="PagedResultDto{T}" /> object.
		/// </summary>
		public PagedResultDto()
		{
			// Note: Needed for serialization.
		}

		/// <summary>
		///     Creates a new <see cref="PagedResultDto{T}" /> object.
		/// </summary>
		/// <param name="totalCount">Total count of Items</param>
		/// <param name="items">List of items in current page</param>
		public PagedResultDto(long totalCount, IReadOnlyList<T> items)
			: base(items)
		{
			this.TotalCount = totalCount;
		}

		/// <inheritdoc />
		public long TotalCount { get; set; }
	}
}
