namespace Fluxera.Extensions.Hosting.Modules.Principal
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class PrincipalAccessor : IPrincipalAccessor
	{
		private readonly IEnumerable<IPrincipalProvider> contributors;

		public PrincipalAccessor(IEnumerable<IPrincipalProvider> contributors)
		{
			this.contributors = Guard.Against.Null(contributors);
		}

		public ClaimsPrincipal User
		{
			get
			{
				ClaimsPrincipal user = null;

				if(contributors.Any())
				{
					foreach(IPrincipalProvider contributor in contributors)
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

		/// <inheritdoc />
		public async Task<string> GetAccessTokenAsync()
		{
			string accessToken = null;

			if(contributors.Any())
			{
				foreach(IPrincipalProvider contributor in contributors)
				{
					accessToken = await contributor.GetAccessTokenAsync();
					if(!string.IsNullOrWhiteSpace(accessToken))
					{
						break;
					}
				}
			}

			return accessToken;
		}
	}
}
