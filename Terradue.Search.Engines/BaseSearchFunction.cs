using System;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Engines
{
    internal class BaseSearchFunction : ISearchFunction
    {
        private readonly string identifier;
        private ISearchCriterionSet searchCriterionSet;
        private Type type;

        private Func<ISearchQuery, ISearchTask> searchFunction;

        public BaseSearchFunction(string identifier, ISearchCriterionSet searchCriterionSet, Type type, Func<ISearchQuery, ISearchTask> searchFunction)
        {
            this.identifier = identifier;
            this.searchCriterionSet = searchCriterionSet;
            this.type = type;
            this.searchFunction = searchFunction;
        }

        public string Identifier => identifier;

        public ISearchCriterionSet SearchCriterionSet => searchCriterionSet;

        public Type ResultType => type;

        public ISearchTask CreateSearch(ISearchQuery query) => searchFunction(query);
    }
}