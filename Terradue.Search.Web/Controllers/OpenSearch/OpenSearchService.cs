using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Terradue.Search.Engines;
using Terradue.Search.Engines.Parameters.Common;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;
using Terradue.Search.Model.Query;
using Terradue.Search.Web.Controllers.Xml;
using Terradue.Search.Web.Formatters.OpenSearch;
using Terradue.Search.Web.Model.OpenSearch.Description;

namespace Terradue.Search.Web.Controllers.OpenSearch
{
    public class OpenSearchService : IOpenSearchService
    {
        public static readonly Dictionary<string, XmlQualifiedName> OSBASEKEYTABLE = new List<KeyValuePair<string, XmlQualifiedName>>(){
            new KeyValuePair<string, XmlQualifiedName>("q", OpenSearchHelpers.OSXN_SEARCHTERMS),
            new KeyValuePair<string, XmlQualifiedName>("count", OpenSearchHelpers.OSXN_COUNT),
            new KeyValuePair<string, XmlQualifiedName>("startIndex", OpenSearchHelpers.OSXN_STARTINDEX),
            new KeyValuePair<string, XmlQualifiedName>("startPage", OpenSearchHelpers.OSXN_STARTPAGE),
            new KeyValuePair<string, XmlQualifiedName>("lang", OpenSearchHelpers.OSXN_LANGUAGE),
        }.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        private readonly ILogger<OpenSearchService> _logger;

        public OpenSearchService(ILogger<OpenSearchService> logger)
        {
            _logger = logger;
        }

        public OpenSearchFormatterSelector FormatterSelector => throw new NotImplementedException();

        public static void Configure(IServiceCollection services)
        {
            MvcOptions mvcOptions = null;
            services.AddMvc(options =>
            {
                AddConfiguredFormatters(options.OutputFormatters);
                mvcOptions = options;
            });
            services.TryAddSingleton<OpenSearchFormatterSelector, OpenSearchFormatterSelector>();
        }

        private static void AddConfiguredFormatters(FormatterCollection<IOutputFormatter> outputFormatters)
        {
            outputFormatters.Insert(0, new OpenSearchDescriptionFormatter());
        }

        public Dictionary<string, XmlQualifiedName> GetKeyParameterTable() => OpenSearchService.OSBASEKEYTABLE;

        public async Task<IActionResult> Search(ISearchFunction searchFunction, HttpRequest request)
        {
            ISearchQuery query = OpenSearchHelpers.CreateSearchQuery(request.Query, searchFunction);
            ISearchTask searchTask = searchFunction.CreateSearch(query);
            if ( searchTask is IResultSearchTask ){
                return new ObjectResult(await ((IResultSearchTask)searchTask).SearchResult());
            }
            else {
                await searchTask.Search();
                return new OkResult();
            }

        }
    }
}