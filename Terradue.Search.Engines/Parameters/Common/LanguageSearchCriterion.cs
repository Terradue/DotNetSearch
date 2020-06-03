using System.Collections.Generic;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Engines.Parameters.Common
{
    public class LanguageSearchCriterion : TypedCriterion<long>
    {

        public LanguageSearchCriterion(string identifier = "language", string title = "Desired language of the search results") : base (identifier)
        {
            Title = title;
            mandatory = false;
        }

    }
}