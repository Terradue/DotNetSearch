using System.Collections.Generic;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Engines.Parameters.Common
{
    public class StartPageSearchCriterion : TypedCriterion<long>
    {

        public StartPageSearchCriterion(string identifier = "startPage", string title = "Page number of the set of search results") : base (identifier)
        {
            Title = title;
            mandatory = false;
        }

    }
}