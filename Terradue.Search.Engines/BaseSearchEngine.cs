using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terradue.Search.Engines.Parameters.Common;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Engines
{
    public abstract class BaseSearchEngine : ISearchEngine
    {
        public virtual ISearchTask CreateSearch(ISearchQuery query)
        {
            string searchTermsParameter = query.Parameters.GetValueOr<string>("searchTerms", null);
            return CreateSearch(searchTermsParameter);
        }

        public abstract ISearchTask CreateSearch(string queryString);

        public virtual IDictionary<string, ISearchFunction> GetSearchFunctions()
        {
            return new ISearchFunction[]{
                        new BaseSearchFunction(
                            "search",
                            new SearchCriterionSet(
                                new ISearchCriterion[] {
                                    new FreeTextSearchCriterion()
                                }
                            ),
                            typeof(object),
                            CreateSearch
                        )
            }.ToDictionary(sf => sf.Identifier, sf => sf);
        }
    }
}