namespace Fluxera.Extensions.Hosting.Modules.Principal
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class PrincipalAccessor : IPrincipalAccessor
	{
		private readonly IEnumerable<IPrincipalProvider> contributors;

		public PrincipalAccessor(IEnumerable<IPrincipalProvider> contributors)
		{
			// ReSharper disable PossibleMultipleEnumeration
			Guard.Against.Null(contributors, nameof(contributors));

			this.contributors = contributors;
			// ReSharper enable PossibleMultipleEnumeration
		}

		public ClaimsPrincipal User
		{
			get
			{
				ClaimsPrincipal user = null;

				if(this.contributors.Any())
				{
					foreach(IPrincipalProvider contributor in this.contributors)
					{
						user = contributor.User;
						if(user != null)
						{
							break;
						}
					}
				}

				return user;
			}
		}
	}
}
