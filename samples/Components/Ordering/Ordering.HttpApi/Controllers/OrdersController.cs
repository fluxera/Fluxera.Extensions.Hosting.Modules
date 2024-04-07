namespace Ordering.HttpApi.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.OData.Query;
	using Microsoft.AspNetCore.OData.Routing.Controllers;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using Microsoft.OData.Edm;
	using Ordering.Application.Contracts.Orders;
	using ODataOptions = Microsoft.AspNetCore.OData.ODataOptions;

	[ApiController]
	[AllowAnonymous]
	[Route("ordering/Orders")]
	public class OrdersController : ODataController
	{
		private readonly IOrderApplicationService orderApplicationService;
		private readonly IOptions<ODataOptions> options;

		private readonly IList<OrderDto> orders = new List<OrderDto>
		{
			new OrderDto
			{
				EntityId = "38835805a29042499132b635618c37dd",
				OrderNumber = "A123456790"
			},
			new OrderDto
			{
				EntityId = "a48aa043ef8d485491f27e4accdbbb15",
				OrderNumber = "A123456789"
			},
			new OrderDto
			{
				EntityId = "f802e598a1fb47a2975b546a2bd935f1",
				OrderNumber = "A123456788"
			},
			new OrderDto
			{
				EntityId = "86f059a6f20c46a9a36dcfa687d8a1c1",
				OrderNumber = "A123456787"
			}
		};

		public OrdersController(IOrderApplicationService orderApplicationService, IOptions<ODataOptions> options)
		{
			this.orderApplicationService = orderApplicationService;
			this.options = options;
		}

		[HttpGet]
		[EnableQuery]
		public async Task<IActionResult> Get(ODataQueryOptions<OrderDto> queryOptions)
		{
			//IQueryCollection queryCollection = this.HttpContext.Request.Query;
			//IDictionary<string, string> queryParameters = queryCollection.ToDictionary(x => x.Key, x => x.Value.ToString());

			//IServiceProvider serviceProvider = this.options.Value.GetRouteServices("odata");
			//List<IEdmModel> edmModels = serviceProvider.GetServices<IEdmModel>().ToList();

			//Dictionary<string, string> options = new Dictionary<string, string>
			//{
			//	//{"$select"  , "ID"                          },
			//	//{"$expand"  , "ProductDetail"               },
			//	//{"$filter"  , "OrderNumber eq 'A123456789'" },
			//	//{"$orderby" , "ID desc"                     },
			//	//{"$top"     , "1"                           },
			//	//{"$count"   , "true"                        },
			//	//{"$search"  , "tom"                         },
			//};

			//IEdmType edmType = edmModels[1].FindDeclaredType("Default.Order");
			//IEdmType edmType2 = edmModel.FindType("Default.Order");

			//IEdmNavigationSource edmNavigationSource = edmModel.FindDeclaredEntitySet("Orders");

			//ODataQueryOptionParser parser = new ODataQueryOptionParser(edmModel, edmType, edmNavigationSource, queryParameters);

			//FilterClause filterClause = parser.ParseFilter();

			//ClrTypeAnnotation annotation = edmModel.GetAnnotationValue<ClrTypeAnnotation>(edmType.AsElementType());

			//ODataPath path = new ODataPath(new FilterSegment(filterClause.Expression, filterClause.RangeVariable, edmNavigationSource));
			//ODataQueryContext context = new ODataQueryContext(edmModel, annotation.ClrType, path);
			//ODataQueryOptions<OrderDto> queryOptions = new ODataQueryOptions<OrderDto>(context, this.HttpContext.Request);

			IQueryable queryable = queryOptions.ApplyTo(this.orders.AsQueryable());
			
			//ResultDto<OrderDto[]> result = await this.orderApplicationService.GetOrdersAsync();

			//if(result.IsFailed)
			//{
			//	return this.StatusCode((int)HttpStatusCode.InternalServerError, result.Errors);
			//}

			//if(result.Value is null)
			//{
			//	return this.NotFound();
			//}

			return this.Ok(this.orders.AsQueryable());
		}
	}
}
