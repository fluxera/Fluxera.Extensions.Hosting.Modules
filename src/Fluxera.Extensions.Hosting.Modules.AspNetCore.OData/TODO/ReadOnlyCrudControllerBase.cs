//namespace Fluxera.Extensions.Hosting.Modules.AspNetCore.OData
//{
//	using System;
//	using System.Collections.Generic;
//	using System.Linq.Expressions;
//	using System.Threading;
//	using System.Threading.Tasks;
//	using JetBrains.Annotations;
//	using Microsoft.AspNetCore.Mvc;
//	using Microsoft.AspNetCore.OData.Abstracts;
//	using Microsoft.AspNetCore.OData.Extensions;
//	using Microsoft.AspNetCore.OData.Query;
//	using Microsoft.AspNetCore.OData.Query.Validator;
//	using Microsoft.AspNetCore.OData.Routing.Controllers;
//	using Microsoft.OData.UriParser;
//	using ODataPath = Microsoft.AspNet.OData.Routing.ODataPath;

//	[PublicAPI]
//	[ODataFormatting]
//	public abstract class ReadOnlyCrudControllerBase<TDto> : ODataController
//		where TDto : class, IEntityDto
//	{
//		private IReadOnlyCrudApplicationService<TDto> applicationService;

//		protected ReadOnlyCrudControllerBase(IReadOnlyCrudApplicationService<TDto> applicationService)
//		{
//			this.applicationService = applicationService;

//			this.ValidationSettings = new ODataValidationSettings
//			{
//				AllowedArithmeticOperators = AllowedArithmeticOperators.All,
//				AllowedFunctions = AllowedFunctions.All,
//				AllowedLogicalOperators = AllowedLogicalOperators.All,
//				AllowedQueryOptions = AllowedQueryOptions.All
//			};
//		}

//		protected ODataValidationSettings ValidationSettings { get; set; }

//		protected virtual bool SupportsGet => true;

//		protected virtual bool SupportsFind => true;

//		private bool IsCountRequest
//		{
//			get
//			{
//				IODataFeature feature = this.Request.HttpContext.ODataFeature();
//				ODataPath path = feature.Path;
//				ODataPathSegment lastSegment = path.Segments.LastOrDefault();
//				return lastSegment is CountSegment;
//			}
//		}

//		// TODO: $select, $expand, any(), max(), min()
//		// GET: odata/{Items}
//		// GET: odata/{Items}(5)
//		[HttpGet]
//		[EnableQuery]
//		public virtual async Task<IActionResult> Get(ODataQueryOptions<TDto> queryOptions, CancellationToken cancellationToken = default)
//		{
//			// Should execute a get by id.
//			if(this.ControllerContext.RouteData.Values["key"] is string key)
//			{
//				// Get by ID
//				this.TryThrowNotSupported(this.SupportsGet, "get by id");

//				// Get the item by id.
//				try
//				{
//					TDto item = await this.applicationService.GetAsync(key, cancellationToken);

//					// Check if the item exists.
//					if(item == null)
//					{
//						return this.NotFound();
//					}

//					return this.Ok(item);
//				}
//				catch(NotSupportedException)
//				{
//					return this.NotFound();
//				}
//			}

//			this.TryThrowNotSupported(this.SupportsFind, "find");

//			IQueryOptions<TDto> options = QueryOptions.CreateFor<TDto>();
//			if(queryOptions.OrderBy != null)
//			{
//				options = queryOptions.OrderBy.ApplyTo(options);
//			}

//			if(queryOptions.Skip != null)
//			{
//				options = queryOptions.Skip.ApplyTo(options);
//			}

//			if(queryOptions.Top != null)
//			{
//				options = queryOptions.Top.ApplyTo(options);
//			}

//			Expression<Func<TDto, bool>> predicate = queryOptions.Filter?.ToExpression<TDto>() ?? (x => true);

//			// TODO: This is currently correct but will fail when query options are mapped.
//			//if (this.IsCountRequest)
//			//{
//			//	long countResult = await this.applicationService.CountAsync(predicate, cancellationToken);
//			//	return this.Ok(countResult);
//			//}
//			//else
//			//{
//			//if (queryOptions.Top != null)
//			//{
//			//    int numResult;
//			//    int pageSize = queryOptions.Top.Value;

//			//    string scheme = Request.Scheme;
//			//    HostString host1 = Request.Host;
//			//    string host2 = host1.Host;
//			//    UriBuilder uriBuilder = new UriBuilder(scheme, host2)
//			//    {
//			//        Path = (Request.PathBase + Request.Path).ToUriComponent()
//			//    };
//			//    IEnumerable<KeyValuePair<string, string>> queryParameters =
//			//        Request.Query.SelectMany<KeyValuePair<string, StringValues>, string, KeyValuePair<string, string>>(
//			//            (Func<KeyValuePair<string, StringValues>, IEnumerable<string>>)(kvp => (IEnumerable<string>)kvp.Value),
//			//            (Func<KeyValuePair<string, StringValues>, string, KeyValuePair<string, string>>)((kvp, value) =>
//			//                new KeyValuePair<string, string>(kvp.Key, value)));

//			//    StringBuilder stringBuilder = new StringBuilder();
//			//    int num = pageSize;
//			//    string str1 = null;
//			//    bool flag = string.IsNullOrWhiteSpace(str1);
//			//    foreach (KeyValuePair<string, string> queryParameter in queryParameters)
//			//    {
//			//        string lowerInvariant = queryParameter.Key.ToLowerInvariant();
//			//        string str2 = queryParameter.Value;
//			//        if (lowerInvariant != null)
//			//        {
//			//            if (!(lowerInvariant == "$top"))
//			//            {
//			//                if (!(lowerInvariant == "$skip"))
//			//                {
//			//                    if (lowerInvariant == "$skiptoken")
//			//                        continue;
//			//                }
//			//                else
//			//                {
//			//                    if (flag && int.TryParse(str2, out numResult))
//			//                    {
//			//                        num += numResult;
//			//                        continue;
//			//                    }
//			//                    continue;
//			//                }
//			//            }
//			//            else
//			//            {
//			//                if (int.TryParse(str2, out numResult))
//			//                {
//			//                    if (numResult <= pageSize)
//			//                        //return (Uri)null;
//			//                        break;
//			//                    str2 = (numResult - pageSize).ToString((IFormatProvider)CultureInfo.InvariantCulture);
//			//                }
//			//            }
//			//        }
//			//        string str3 = lowerInvariant.Length <= 0 || lowerInvariant[0] != '$'
//			//            ? Uri.EscapeDataString(lowerInvariant)
//			//            : "$" + Uri.EscapeDataString(lowerInvariant.Substring(1));
//			//        string str4 = Uri.EscapeDataString(str2);
//			//        stringBuilder.Append(str3);
//			//        stringBuilder.Append('=');
//			//        stringBuilder.Append(str4);
//			//        stringBuilder.Append('&');
//			//    }

//			//    if (flag)
//			//        stringBuilder.AppendFormat("$skip={0}", (object)num);
//			//    else
//			//        stringBuilder.AppendFormat("$skiptoken={0}", (object)str1);

//			//    Uri uri = new UriBuilder(uriBuilder.Uri)
//			//    {
//			//        Query = stringBuilder.ToString(),
//			//    }.Uri;

//			//    Uri nextPageLink = this.Request.GetNextPageLink(pageSize);
//			//    this.Request.ODataFeature().NextLink = nextPageLink;
//			//}

//			// Return the query result.
//			try
//			{
//				IReadOnlyList<TDto> result = await this.applicationService.FindManyAsync(predicate, options, cancellationToken);
//				return this.Ok(result);
//			}
//			catch(NotSupportedException)
//			{
//				return this.NotFound();
//			}
//		}

//		protected void TryThrowNotSupported(bool condition, string name)
//		{
//			if(!condition)
//			{
//				throw new NotSupportedException($"This resource does not support {name}.");
//			}
//		}
//	}
//}


