using System;
using System.Collections.Generic;

namespace Terradue.Search.Model.Parameters
{
    public interface ISearchCriterionSet : IEnumerable<ISearchCriterion>
    {
        bool Contains(string identifier);

        bool Remove(string identifier);

        void Add(ISearchCriterion parameter);

        void Insert(int pos, ISearchCriterion parameter);

        int GetPosition(string identifier);

        ISearchCriterion GetCriterion(string identifier);

    }
}
