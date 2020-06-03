using System;
using System.Collections.Generic;
using System.Linq;
using Terradue.Search.Engines.Simple;
using Terradue.Search.Model.Parameters;
using Terradue.Search.Model.Query;
using Xunit;

namespace Terradue.Search.Tests.Engine
{
    public class SimpleSearchTests
    {
        [Fact]
        public async void SimpleEnumerableStringSearchTest()
        {
            string prefix = "test";
            var testEnumerable = TestsHelper.CreateStringEnumerable(100, prefix);
            EnumerableSearchEngine<string> enumerableSearchEngine = new EnumerableSearchEngine<string>(testEnumerable);
            SearchQuery query = new SearchQuery(new SearchParameterSet(
                new List<ISearchParameter>() { new TypedParameter<string>("searchTerms", "test") }
            ));

            var searchTask = enumerableSearchEngine.CreateSearch(query) as EnumerableResultSearchTask<string>;

            Assert.Equal(testEnumerable, (await searchTask.SearchResults<string>()));

            query = new SearchQuery(new SearchParameterSet(
                new List<TypedParameter<string>>() { new TypedParameter<string>("searchTerms", "1") }
            ));
            searchTask = enumerableSearchEngine.CreateSearch(query) as EnumerableResultSearchTask<string>;

            Assert.Equal(20, (await searchTask.SearchResults<string>()).Count());

            searchTask = enumerableSearchEngine.CreateSearch("1") as EnumerableResultSearchTask<string>;

            Assert.Equal(20, (await searchTask.SearchResults<string>()).Count());
        }

        [Fact]
        public async void PaginatedEnumerableStringSearchTest()
        {
            string prefix = "test";
            var testEnumerable = TestsHelper.CreateStringEnumerable(100, prefix);
            PaginableSearchEngine<string> enumerableSearchEngine = new PaginableSearchEngine<string>(new EnumerableSearchEngine<string>(testEnumerable));
            SearchQuery query = new SearchQuery(new SearchParameterSet(
                new List<TypedParameter<string>>() { new TypedParameter<string>("searchTerms", prefix) }
            ));
            EnumerableResultSearchTask<string> resultTask = (EnumerableResultSearchTask<string>)enumerableSearchEngine.CreateSearch(query) as EnumerableResultSearchTask<string>;
            Assert.Equal(testEnumerable.Take(20), (await resultTask.SearchResults<string>()));

            query = new SearchQuery(new SearchParameterSet(
                new List<TypedParameter<string>>() { new TypedParameter<string>("searchTerms", "1") }
            ));
            resultTask = (EnumerableResultSearchTask<string>)enumerableSearchEngine.CreateSearch(query);

            Assert.Equal(20, (await resultTask.SearchResults<string>()).Count());

            resultTask = (EnumerableResultSearchTask<string>)enumerableSearchEngine.CreateSearch("2");

            Assert.Equal(19, (await resultTask.SearchResults<string>()).Count());

            resultTask = (EnumerableResultSearchTask<string>)enumerableSearchEngine.CreateSearch("test");

            Assert.Equal(20, (await resultTask.SearchResults<string>()).Count());

            query = new SearchQuery(new SearchParameterSet());

            resultTask = (EnumerableResultSearchTask<string>)enumerableSearchEngine.CreateSearch(query);

            Assert.Equal(20, (await resultTask.SearchResults<string>()).Count());

            resultTask = (EnumerableResultSearchTask<string>)enumerableSearchEngine.GetPage("", new PaginationParameters(91, 1, 20));

            Assert.Equal(10, (await resultTask.SearchResults<string>()).Count());

            resultTask = (EnumerableResultSearchTask<string>)enumerableSearchEngine.GetPage("", new PaginationParameters(0, 2, 50));

            Assert.Equal(50, (await resultTask.SearchResults<string>()).Count());
        }
    }
}
