namespace Fluxera.Extensions.Hosting.Modules.Domain.Interceptors
{
	using System;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using Fluxera.Entity;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using Fluxera.Linq.Expressions;
	using Fluxera.Repository.Interception;
	using Fluxera.Repository.Query;
	using Fluxera.Repository.Specifications;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for interceptors that should handle multi tenant entities.
	/// </summary>
	/// <typeparam name="TAggregateRoot"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public abstract class MultiTenancyInterceptorBase<TAggregateRoot, TKey> : InterceptorBase<TAggregateRoot, TKey>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>, IMultiTenancyObject
	{
		/// <summary>
		///     Gets the ID of the current tenant.
		/// </summary>
		protected abstract string TenantID { get; }

		/// <inheritdoc />
		public override Task BeforeAddAsync(TAggregateRoot item, InterceptionEvent e)
		{
			string tenantID = this.TenantID;

			if(string.IsNullOrWhiteSpace(tenantID))
			{
				throw new InvalidOperationException("The tenant for the item to add was empty.");
			}

			item.TenantID = tenantID;

			return base.BeforeAddAsync(item, e);
		}

		/// <inheritdoc />
		public override Task BeforeUpdateAsync(TAggregateRoot item, InterceptionEvent e)
		{
			string tenantID = this.TenantID;

			if(string.IsNullOrWhiteSpace(tenantID))
			{
				throw new InvalidOperationException("The tenant for the item to update was empty.");
			}

			if(string.IsNullOrWhiteSpace(item.TenantID))
			{
				throw new InvalidOperationException("The item to update has no tenant set.");
			}

			if(item.TenantID != tenantID)
			{
				throw new InvalidOperationException("The item to update doesn't belong to the current tenant.");
			}

			return base.BeforeUpdateAsync(item, e);
		}

		/// <inheritdoc />
		public override Task BeforeRemoveAsync(TAggregateRoot item, InterceptionEvent e)
		{
			string tenantID = this.TenantID;

			if(string.IsNullOrWhiteSpace(tenantID))
			{
				throw new InvalidOperationException("The tenant for the item to remove was empty.");
			}

			if(string.IsNullOrWhiteSpace(item.TenantID))
			{
				throw new InvalidOperationException("The item to remove has no tenant set.");
			}

			if(item.TenantID != tenantID)
			{
				throw new InvalidOperationException("The item to remove doesn't belong to the current tenant.");
			}

			return base.BeforeRemoveAsync(item, e);
		}

		/// <inheritdoc />
		public override Task<Expression<Func<TAggregateRoot, bool>>> BeforeRemoveRangeAsync(Expression<Func<TAggregateRoot, bool>> predicate, InterceptionEvent e)
		{
			string tenantID = this.TenantID;

			if(string.IsNullOrWhiteSpace(tenantID))
			{
				throw new InvalidOperationException("The tenant for the items to remove was empty.");
			}

			Expression<Func<TAggregateRoot, bool>> tenantPredicate = predicate.AndAlso(x => x.TenantID == tenantID);

			return base.BeforeRemoveRangeAsync(tenantPredicate, e);
		}

		/// <inheritdoc />
		public override Task<ISpecification<TAggregateRoot>> BeforeRemoveRangeAsync(ISpecification<TAggregateRoot> specification, InterceptionEvent e)
		{
			string tenantID = this.TenantID;

			if(string.IsNullOrWhiteSpace(tenantID))
			{
				throw new InvalidOperationException("The tenant for the items to remove was empty.");
			}

			ISpecification<TAggregateRoot> tenantSpecification = specification.AndAlso(x => x.TenantID == tenantID);

			return base.BeforeRemoveRangeAsync(tenantSpecification, e);
		}

		/// <inheritdoc />
		public override Task<ISpecification<TAggregateRoot>> BeforeFindAsync(ISpecification<TAggregateRoot> specification, IQueryOptions<TAggregateRoot> queryOptions)
		{
			string tenantID = this.TenantID;

			if(string.IsNullOrWhiteSpace(tenantID))
			{
				throw new InvalidOperationException("The tenant for the find predicate was empty.");
			}

			ISpecification<TAggregateRoot> tenantSpecification = specification.AndAlso(x => x.TenantID == tenantID);

			return base.BeforeFindAsync(tenantSpecification, queryOptions);
		}

		/// <inheritdoc />
		public override Task<Expression<Func<TAggregateRoot, bool>>> BeforeFindAsync(Expression<Func<TAggregateRoot, bool>> predicate, IQueryOptions<TAggregateRoot> queryOptions)
		{
			string tenantID = this.TenantID;

			if(string.IsNullOrWhiteSpace(tenantID))
			{
				throw new InvalidOperationException("The tenant for the find predicate was empty.");
			}

			Expression<Func<TAggregateRoot, bool>> tenantPredicate = predicate.AndAlso(x => x.TenantID == tenantID);

			return base.BeforeFindAsync(tenantPredicate, queryOptions);
		}
	}
}
