using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Terradue.Search.Engines;
using Terradue.Search.Model;
using Terradue.Search.Web.Controllers.Xml;
using Terradue.Search.Web.Model.OpenSearch.Description;

namespace Terradue.Search.Web.Controllers.OpenSearch
{
    public abstract class OpenSearchControllerBase : ControllerBase, IOpenSearchContext
    {

        public abstract ISearchEngine SearchEngine { get; }

        private readonly IOpenSearchService openSearchService;

        private Dictionary<string, XmlQualifiedName> queryStringKeyTable;

        public OpenSearchControllerBase(IOpenSearchService openSearchService)
        {
            this.openSearchService = openSearchService;
            this.queryStringKeyTable = new Dictionary<string, XmlQualifiedName>(openSearchService.GetKeyParameterTable());
        }

        public Uri BaseSearchUrlTemplate
        {
            get
            {

                MethodBase searchMethod = this.GetType().GetMethod("Search");
                RouteAttribute searchRouteAttribute = (RouteAttribute)searchMethod.GetCustomAttributes(typeof(RouteAttribute), true)[0];
                RouteAttribute controllerRouteAttribute = (RouteAttribute)this.GetType().GetCustomAttributes(typeof(RouteAttribute), true)[0];

                string searchRoute = searchRouteAttribute != null ? searchRouteAttribute.Template : "search";
                string controllerRoute = controllerRouteAttribute != null ? controllerRouteAttribute.Template : ControllerContext.RouteData.Values["controller"].ToString().ToLower();

                return new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}/{controllerRoute}/{searchRoute}");
            }
        }

        public IOpenSearchService OpenSearchService => openSearchService;

    }
}
