namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Repository;

	internal sealed class RepositoryAggregatesBuilder : IRepositoryAggregatesBuilder
	{
		private readonly IRepositoryOptionsBuilder repositoryOptionsBuilder;

		public RepositoryAggregatesBuilder(IRepositoryOptionsBuilder repositoryOptionsBuilder)
		{
			this.repositoryOptionsBuilder = repositoryOptionsBuilder;
		}

		/// <inheritdoc />
		public IRepositoryAggregatesBuilder UseFor(IEnumerable<Assembly> assemblies)
		{
			this.repositoryOptionsBuilder.UseFor(assemblies);
			return this;
		}

		/// <inheritdoc />
		public IRepositoryAggregatesBuilder UseFor(Assembly assembly)
		{
			this.repositoryOptionsBuilder.UseFor(assembly);
			return this;
		}

		/// <inheritdoc />
		public IRepositoryAggregatesBuilder UseFor(IEnumerable<Type> types)
		{
			this.repositoryOptionsBuilder.UseFor(types);
			return this;
		}

		/// <inheritdoc />
		public IRepositoryAggregatesBuilder UseFor(Type type)
		{
			this.repositoryOptionsBuilder.UseFor(type);
			return this;
		}

		/// <inheritdoc />
		public IRepositoryAggregatesBuilder UseFor<TAggregateRoot>()
		{
			this.repositoryOptionsBuilder.UseFor<TAggregateRoot>();
			return this;
		}
	}
}
