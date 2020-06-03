using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terradue.Search.Engines.Parameters.Common;
using Terradue.Search.Engines.Utils;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Engines.Simple
{
    public class PaginableSearchEngine<TResult> : BaseSearchEngine
    {
        private readonly EnumerableSearchEngine<TResult> searchEngine;

        public PaginableSearchEngine(EnumerableSearchEngine<TResult> searchEngine)
        {
            this.searchEngine = searchEngine;
        }

        public override IDictionary<string, ISearchFunction> GetSearchFunctions()
        {
            return new ISearchFunction[]{
                        new BaseSearchFunction(
                            "search",
                            new SearchCriterionSet(
                                new ISearchCriterion[] {
                                    new FreeTextSearchCriterion(),
                                    new PageSizeSearchCriterion(),
                                    new StartIndexSearchCriterion(),
                                    new StartPageSearchCriterion()
                                }
                            ),
                            typeof(object),
                            CreateSearch
                        )
            }.ToDictionary(sf => sf.Identifier, sf => sf);
        }

        public override ISearchTask CreateSearch(ISearchQuery query)
        {
            string searchTermsParameter = query.Parameters.GetValueOr<string>("searchTerms", null);
            long startPageParameter = query.Parameters.GetValueOr<long>("startPage", PaginationParameters.Default.StartPage);
            long startIndexParameter = query.Parameters.GetValueOr<long>("startIndex", PaginationParameters.Default.StartIndex);
            int pageSizeParameter = query.Parameters.GetValueOr<int>("pageSize", PaginationParameters.Default.PageSize);
            PaginationParameters paginationParameters = new PaginationParameters(startIndexParameter, startPageParameter, pageSizeParameter);
            return (ISearchTask)GetPage(searchTermsParameter, paginationParameters);
        }

        public override ISearchTask CreateSearch(string searchTerms)
        {
            return (ISearchTask)GetPage(searchTerms, PaginationParameters.Default);
        }

        public EnumerableResultSearchTask<TResult> GetPage(string searchTerms, PaginationParameters paginationParams)
        {
            EnumerableResultSearchTask<TResult> searchTask = searchEngine.SearchMany(searchTerms);

            return new EnumerableResultSearchTask<TResult>(
                searchTask.SearchResult<IEnumerable<TResult>>().ContinueWith(searchResults =>
            {
                MemoryPaginatedList<TResult> paginatedList = new MemoryPaginatedList<TResult>(searchResults.Result);
                paginatedList.StartIndex = Convert.ToInt32(paginationParams.StartIndex);
                paginatedList.PageNo = Convert.ToInt32(paginationParams.StartPage);
                paginatedList.PageSize = Convert.ToInt32(paginationParams.PageSize);
                return paginatedList.GetCurrentPage();
            }));
        }
    }
}
