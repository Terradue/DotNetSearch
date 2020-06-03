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
    public class OpenSearchControllerAutomatic : OpenSearchControllerBase
    {

        public override ISearchEngine SearchEngine { get; }

        public OpenSearchControllerAutomatic(ISearchEngine searchEngine, IOpenSearchService openSearchService) : base(openSearchService)
        {
            SearchEngine = searchEngine;
        }

        [HttpGet]
        [Route("{functionId}")]
        public virtual async Task<IActionResult> Search(string functionId)
        {
            return await OpenSearchService.Search(SearchEngine.GetSearchFunctions()[functionId], Request);
        }

        [HttpGet]
        [Route("description")]
        public virtual ActionResult<OpenSearchDescription> Description()
        {
            return OpenSearchHelpers.GenerateOpenSearchDescription(SearchEngine, HttpContext, this);
        }

    }
}
