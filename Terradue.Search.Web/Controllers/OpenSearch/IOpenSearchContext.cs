using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.AspNetCore.Mvc.Formatters;
using Terradue.Search.Web.Controllers.Xml;

namespace Terradue.Search.Web.Controllers.OpenSearch
{
    public interface IOpenSearchContext
    {

        Uri BaseSearchUrlTemplate { get; }
        
    }
}