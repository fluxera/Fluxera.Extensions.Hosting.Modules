namespace Fluxera.Extensions.Hosting.Modules.AutoMapper
{
	using System;
	using System.Linq.Expressions;
	using global::AutoMapper;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="IMappingExpression{TSource,TDestination}" /> type.
	/// </summary>
	[PublicAPI]
	public static class AutoMapperExpressionExtensions
	{
		/// <summary>
		///     Ignores the member when mapping.
		/// </summary>
		/// <typeparam name="TDestination"></typeparam>
		/// <typeparam name="TMember"></typeparam>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="mappingExpression"></param>
		/// <param name="destinationMember"></param>
		/// <returns></returns>
		public static IMappingExpression<TDestination, TMember> Ignore<TDestination, TMember, TResult>(
			this IMappingExpression<TDestination, TMember> mappingExpression,
			Expression<Func<TMember, TResult>> destinationMember)
		{
			return mappingExpression.ForMember(destinationMember, opts => opts.Ignore());
		}

		/// <summary>
		///		Creates mapping from <typeparamref name="TDestination" /> to <typeparamref name="TSource"/>,
		///		enabling validation so that <see cref="MapperConfiguration.AssertConfigurationIsValid"/>
		///		correctly identifies any missed reverse mappings.
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TDestination"></typeparam>
		/// <param name="mappingExpression"></param>
		/// <returns></returns>
		public static IMappingExpression<TDestination, TSource> CreateValidatedReverseMap<TSource, TDestination>(
			this IMappingExpression<TSource, TDestination> mappingExpression)
		{
			return mappingExpression.ReverseMap().ValidateMemberList(MemberList.Source);
		}
	}
}
