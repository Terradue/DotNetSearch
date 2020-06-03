using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;
using Terradue.Search.Web.Controllers.Xml;
using Terradue.Search.Web.Formatters.OpenSearch;
using Terradue.Search.Web.Model.OpenSearch.Description;

namespace Terradue.Search.Web.Controllers.OpenSearch
{
    public interface IOpenSearchService
    {
        Dictionary<string, XmlQualifiedName> GetKeyParameterTable();

        Task<IActionResult> Search(ISearchFunction searchFunction, HttpRequest request);

        OpenSearchFormatterSelector FormatterSelector { get; }

    }
}
