namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.HttpApi.OData
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Dtos;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services;
	using Fluxera.Extensions.Hosting.Modules.Application.Contracts.Services.Query;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.OData.Abstracts;
	using Microsoft.AspNetCore.OData.Extensions;
	using Microsoft.AspNetCore.OData.Query;
	using Microsoft.AspNetCore.OData.Routing.Controllers;
	using Microsoft.OData.UriParser;

	/// <summary>
	///     A base controller for OData read-only operations.
	/// </summary>
	/// <typeparam name="TDto"></typeparam>
	[PublicAPI]
	public abstract class ReadOnlyCrudControllerBase<TDto> : ODataController
		where TDto : class, IEntityDto
	{
		private IReadOnlyCrudApplicationService<TDto> applicationService;

		/// <summary>
		///     Initializes a new instance of the <see cref="ReadOnlyCrudControllerBase{T}" /> type.
		/// </summary>
		/// <param name="applicationService"></param>
		protected ReadOnlyCrudControllerBase(IReadOnlyCrudApplicationService<TDto> applicationService)
		{
			this.applicationService = applicationService;
		}

		/// <summary>
		///     Flag, indicating if the controller supports the get-by-id operation.
		/// </summary>
		protected virtual bool SupportsGet => true;

		/// <summary>
		///     Flag, indicating if the controller supports the count operation.
		/// </summary>
		protected virtual bool SupportsCount => true;

		/// <summary>
		///     Flag, indicating if the controller supports the find operation.
		/// </summary>
		protected virtual bool SupportsFind => true;

		private bool IsCountRequest
		{
			get
			{
				IODataFeature feature = this.Request.HttpContext.ODataFeature();
				ODataPath path = feature.Path;
				ODataPathSegment lastSegment = path.LastSegment;
				return lastSegment is CountSegment;
			}
		}

		private bool IsGetByKeyRequest =>
			this.ControllerContext.RouteData.Values.ContainsKey("key") &&
			this.ControllerContext.RouteData.Values["key"] is string;

		// GET: odata/{Items}
		// GET: odata/{Items}(5)
		/// <summary>
		///     An OData Get endpoint that supports get-by-id, count and find operations depending on the
		///     route and query parameters.
		/// </summary>
		/// <param name="queryOptions"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpGet]
		[EnableQuery]
		public virtual async Task<IActionResult> Get(ODataQueryOptions<TDto> queryOptions, CancellationToken cancellationToken = default)
		{
			if(this.IsGetByKeyRequest)
			{
				string key = this.ControllerContext.RouteData.Values["key"] as string;
				return await this.GetByKeyAsync(key, cancellationToken);
			}

			if(this.IsCountRequest)
			{
				return await this.GetCountAsync(queryOptions, cancellationToken);
			}

			return await this.FindAsync(queryOptions, cancellationToken);
		}

		private async Task<IActionResult> GetCountAsync(ODataQueryOptions<TDto> queryOptions, CancellationToken cancellationToken)
		{
			if(!this.SupportsCount)
			{
				return this.NotFound();
			}

			// Get the item count.
			try
			{
				Expression<Func<TDto, bool>> predicate = queryOptions.Filter?.ToExpression<TDto>() ?? (x => true);
				long count = await this.applicationService.CountAsync(predicate, cancellationToken);
				return this.Ok(count);
			}
			catch(NotSupportedException)
			{
				return this.NotFound();
			}
		}

		private async Task<IActionResult> GetByKeyAsync(string key, CancellationToken cancellationToken)
		{
			if(!this.SupportsGet)
			{
				return this.NotFound();
			}

			// Get the item by id.
			try
			{
				TDto item = await this.applicationService.GetAsync(key, cancellationToken);

				// Check if the item exists.
				if(item == null)
				{
					return this.NotFound();
				}

				return this.Ok(item);
			}
			catch(NotSupportedException)
			{
				return this.NotFound();
			}
		}

		private async Task<IActionResult> FindAsync(ODataQueryOptions<TDto> queryOptions, CancellationToken cancellationToken = default)
		{
			if(!this.SupportsFind)
			{
				return this.NotFound();
			}

			// Execute the find query.
			try
			{
				IQueryOptions<TDto> options = queryOptions.OrderBy.ApplyTo<TDto>();
				options = queryOptions.Skip.ApplyTo(options);
				options = queryOptions.Top.ApplyTo(options);

				Expression<Func<TDto, bool>> predicate = queryOptions.Filter?.ToExpression<TDto>() ?? (x => true);
				IReadOnlyCollection<TDto> result = await this.applicationService.FindManyAsync(predicate, options, cancellationToken);
				return this.Ok(result);
			}
			catch(NotSupportedException)
			{
				return this.NotFound();
			}
		}

		// TODO: $select, $expand, any(), max(), min()
		// TODO: This is currently correct but will fail when query options are mapped.
		//else
		//{
		//if (queryOptions.Top != null)
		//{
		//    int numResult;
		//    int pageSize = queryOptions.Top.Value;

		//    string scheme = Request.Scheme;
		//    HostString host1 = Request.Host;
		//    string host2 = host1.Host;
		//    UriBuilder uriBuilder = new UriBuilder(scheme, host2)
		//    {
		//        Path = (Request.PathBase + Request.Path).ToUriComponent()
		//    };
		//    IEnumerable<KeyValuePair<string, string>> queryParameters =
		//        Request.Query.SelectMany<KeyValuePair<string, StringValues>, string, KeyValuePair<string, string>>(
		//            (Func<KeyValuePair<string, StringValues>, IEnumerable<string>>)(kvp => (IEnumerable<string>)kvp.Value),
		//            (Func<KeyValuePair<string, StringValues>, string, KeyValuePair<string, string>>)((kvp, value) =>
		//                new KeyValuePair<string, string>(kvp.Key, value)));

		//    StringBuilder stringBuilder = new StringBuilder();
		//    int num = pageSize;
		//    string str1 = null;
		//    bool flag = string.IsNullOrWhiteSpace(str1);
		//    foreach (KeyValuePair<string, string> queryParameter in queryParameters)
		//    {
		//        string lowerInvariant = queryParameter.Key.ToLowerInvariant();
		//        string str2 = queryParameter.Value;
		//        if (lowerInvariant != null)
		//        {
		//            if (!(lowerInvariant == "$top"))
		//            {
		//                if (!(lowerInvariant == "$skip"))
		//                {
		//                    if (lowerInvariant == "$skiptoken")
		//                        continue;
		//                }
		//                else
		//                {
		//                    if (flag && int.TryParse(str2, out numResult))
		//                    {
		//                        num += numResult;
		//                        continue;
		//                    }
		//                    continue;
		//                }
		//            }
		//            else
		//            {
		//                if (int.TryParse(str2, out numResult))
		//                {
		//                    if (numResult <= pageSize)
		//                        //return (Uri)null;
		//                        break;
		//                    str2 = (numResult - pageSize).ToString((IFormatProvider)CultureInfo.InvariantCulture);
		//                }
		//            }
		//        }
		//        string str3 = lowerInvariant.Length <= 0 || lowerInvariant[0] != '$'
		//            ? Uri.EscapeDataString(lowerInvariant)
		//            : "$" + Uri.EscapeDataString(lowerInvariant.Substring(1));
		//        string str4 = Uri.EscapeDataString(str2);
		//        stringBuilder.Append(str3);
		//        stringBuilder.Append('=');
		//        stringBuilder.Append(str4);
		//        stringBuilder.Append('&');
		//    }

		//    if (flag)
		//        stringBuilder.AppendFormat("$skip={0}", (object)num);
		//    else
		//        stringBuilder.AppendFormat("$skiptoken={0}", (object)str1);

		//    Uri uri = new UriBuilder(uriBuilder.Uri)
		//    {
		//        Query = stringBuilder.ToString(),
		//    }.Uri;

		//    Uri nextPageLink = this.Request.GetNextPageLink(pageSize);
		//    this.Request.ODataFeature().NextLink = nextPageLink;
		//}
	}
}
