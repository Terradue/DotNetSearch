using System.Collections.Generic;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Engines.Parameters.Common
{
    public class StartIndexSearchCriterion : TypedCriterion<long>
    {

        public StartIndexSearchCriterion(string identifier = "startIndex", string title = "Index of the first search result") : base (identifier)
        {
            Title = title;
            mandatory = false;
        }

    }
}