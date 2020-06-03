using System.Collections.Generic;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Engines.Parameters.Common
{
    public class PageSizeSearchCriterion : TypedCriterion<long>
    {

        public PageSizeSearchCriterion(string identifier = "pageSize", string title = "Number of search results per page") : base (identifier)
        {
            Title = title;
            mandatory = false;
        }

    }
}