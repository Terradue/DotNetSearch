using System.Collections.Generic;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Engines.Parameters.Common
{
    public class FreeTextSearchCriterion : TypedCriterion<string>
    {

        public FreeTextSearchCriterion(string identifier = "searchTerms", string title = "Free Text Search") : base (identifier)
        {
            Title = title;
            this.mandatory = false;
        }

    }
}