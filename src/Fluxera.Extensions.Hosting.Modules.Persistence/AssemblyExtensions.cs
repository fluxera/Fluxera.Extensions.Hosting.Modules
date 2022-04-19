namespace Fluxera.Extensions.Hosting.Modules.Persistence
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using Fluxera.Entity;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class AssemblyExtensions
	{
		public static IEnumerable<Type> EnumerateAggregates(this Assembly assembly)
		{
			Guard.Against.Null(assembly, nameof(assembly));

			Type[] types = assembly.GetTypes();
			foreach(Type type in types)
			{
				if(type.IsAggregateRoot())
				{
					yield return type;
				}
			}
		}
	}
}
