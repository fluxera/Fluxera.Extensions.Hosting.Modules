//namespace Fluxera.Extensions.Hosting.Modules.Domain.Interceptors
//{
//	using System;
//	using System.Threading.Tasks;
//	using Fluxera.Entity;
//	using Fluxera.Extensions.Hosting.Modules.Domain.Shared;
//	using Fluxera.Repository.Interception;
//	using JetBrains.Annotations;

//	/// <summary>
//	///     A base class for interceptors that should handle soft delete.
//	/// </summary>
//	/// <typeparam name="TAggregateRoot">The aggregate type.</typeparam>
//	/// <typeparam name="TKey">The type of the key.</typeparam>
//	[PublicAPI]
//	public abstract class SoftDeleteInterceptor<TAggregateRoot, TKey> : InterceptorBase<TAggregateRoot, TKey>
//		where TAggregateRoot : AggregateRoot<TAggregateRoot, TKey>, ISoftDeleteObject
//		where TKey : IComparable<TKey>, IEquatable<TKey>
//	{
//		/// <inheritdoc />
//		public override Task BeforeRemoveAsync(TAggregateRoot item, InterceptionEvent e)
//		{
//			item.IsDeleted = true;

//			return base.BeforeRemoveAsync(item, e);
//		}
//	}
//}
