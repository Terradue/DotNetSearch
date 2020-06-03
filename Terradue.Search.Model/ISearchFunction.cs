using System;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Model
{
    public interface ISearchFunction
    {
        string Identifier { get; }

        ISearchCriterionSet SearchCriterionSet { get; }

        Type ResultType { get; }

        ISearchTask CreateSearch(ISearchQuery query);
    }
}