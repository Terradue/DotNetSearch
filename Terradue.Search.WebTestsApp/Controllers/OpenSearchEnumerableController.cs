using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Terradue.Search.Engines;
using Terradue.Search.Engines.Simple;
using Terradue.Search.Model;
using Terradue.Search.Web.Controllers.OpenSearch;
using Terradue.Search.Web.Model.OpenSearch.Description;

namespace Terradue.Search.WebTestsApp.Controllers
{
    [ApiController]
    [Route("opensearch/enumerable")]
    public class OpenSearchEnumerableController : OpenSearchControllerBase
    {
        private readonly ILogger<OpenSearchEnumerableController> _logger;
        private readonly ISearchEngine _engine;

        public OpenSearchEnumerableController(ILogger<OpenSearchEnumerableController> logger, IOpenSearchService openSearchService) : base(openSearchService)
        {
            _logger = logger;
            _engine = new EnumerableSearchEngine<string>(new string[] { "1", "2", "3" });
        }

        public override ISearchEngine SearchEngine => _engine;

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
