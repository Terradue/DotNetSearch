using System.Collections;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Model.Query
{
    public class SearchQuery : ISearchQuery
    {
        private readonly ISearchParameterSet searchParameters;

        public SearchQuery(ISearchParameterSet searchParameters)
        {
            this.searchParameters = searchParameters;
        }

        public ISearchParameterSet Parameters => searchParameters;
    }
}