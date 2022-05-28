namespace Fluxera.Extensions.Hosting.Modules.Domain.Interceptors
{
	using System;
	using System.Linq.Expressions;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Fluxera.Entity;
	using Fluxera.Extensions.Hosting.Modules.Domain.Shared.Model;
	using Fluxera.Repository.Interception;
	using Fluxera.Repository.Specifications;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     A base class for interceptors that should set the time auditing properties.
	/// </summary>
	/// <typeparam name="TAggregateRoot">The aggregate type.</typeparam>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	[PublicAPI]
	public abstract class AuditingInterceptorBase<TAggregateRoot, TKey> : InterceptorBase<TAggregateRoot, TKey>
		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>, IAuditedObject
		where TKey : IComparable<TKey>, IEquatable<TKey>
	{
		/// <summary>
		///     Gets the current timestamp.
		/// </summary>
		protected abstract DateTimeOffset UtcNow { get; }

		/// <summary>
		///     Gets the current user.
		/// </summary>
		protected abstract ClaimsPrincipal Principal { get; }

		/// <inheritdoc />
		public override Task BeforeAddAsync(TAggregateRoot item, InterceptionEvent e)
		{
			this.AuditCreationTimestamp(item);
			this.AuditCreationPrincipal(item);

			return base.BeforeAddAsync(item, e);
		}

		/// <inheritdoc />
		public override Task BeforeUpdateAsync(TAggregateRoot item, InterceptionEvent e)
		{
			this.AuditModificationTimestamp(item);
			this.AuditModificationPrincipal(item);

			return base.BeforeUpdateAsync(item, e);
		}

		/// <inheritdoc />
		public override Task BeforeRemoveAsync(TAggregateRoot item, InterceptionEvent e)
		{
			this.AuditDeletionTimestamp(item);
			this.AuditDeletionPrincipal(item);

			return base.BeforeRemoveAsync(item, e);
		}

		/// <inheritdoc />
		public override Task<Expression<Func<TAggregateRoot, bool>>> BeforeRemoveRangeAsync(Expression<Func<TAggregateRoot, bool>> predicate, InterceptionEvent e)
		{
			throw new InvalidOperationException("The removal of items using a predicate is not allowed when auditing deletes.");
		}

		/// <inheritdoc />
		public override Task<ISpecification<TAggregateRoot>> BeforeRemoveRangeAsync(ISpecification<TAggregateRoot> specification, InterceptionEvent e)
		{
			throw new InvalidOperationException("The removal of items using a predicate is not allowed when auditing deletes.");
		}

		private void AuditCreationTimestamp(TAggregateRoot item)
		{
			item.CreatedAt = this.UtcNow;

			// Modification date is only set on update, because some entities have 
			// a 'in between' state in terms of validation, i.e. the merchant may
			// be valid with less values directly after the account registration
			// but not valid with the existing values on the next update.
			item.LastModifiedAt = null;
		}

		private void AuditModificationTimestamp(TAggregateRoot item)
		{
			item.LastModifiedAt = this.UtcNow;
		}

		private void AuditDeletionTimestamp(TAggregateRoot item)
		{
			item.DeletedAt = this.UtcNow;
		}

		private void AuditCreationPrincipal(TAggregateRoot item)
		{
			ClaimsPrincipal principal = this.Principal;
			if(principal != null)
			{
				item.CreatedBy = principal.GetSubjectId();
				item.LastModifiedBy = null;
			}
		}

		private void AuditModificationPrincipal(TAggregateRoot item)
		{
			ClaimsPrincipal principal = this.Principal;
			if(principal != null)
			{
				item.LastModifiedBy = principal.GetSubjectId();
			}
		}

		private void AuditDeletionPrincipal(TAggregateRoot item)
		{
			ClaimsPrincipal principal = this.Principal;
			if(principal != null)
			{
				item.DeletedBy = principal.GetSubjectId();
			}
		}
	}
}
