namespace Fluxera.Extensions.Hosting.Modules.Application.Contracts.Query
{
	using System.Linq;
	using Fluxera.Utilities.Extensions;

	internal sealed class EmptyQueryOptions<T> : IQueryOptions<T> where T : class
	{
		/// <inheritdoc />
		public IQueryable<T> ApplyTo(IQueryable<T> queryable)
		{
			return queryable;
		}

		/// <inheritdoc />
		public bool IsEmpty()
		{
			return true;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return "QueryOptions<{0}>(Empty)".FormatInvariantWith(typeof(T).Name);
		}
	}
}
