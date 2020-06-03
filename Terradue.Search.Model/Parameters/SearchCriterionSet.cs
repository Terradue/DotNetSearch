using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Terradue.Search.Model.Parameters
{
    public class SearchCriterionSet : ISearchCriterionSet
    {

        private readonly List<ISearchCriterion> set;

        public SearchCriterionSet(IEnumerable<ISearchCriterion> list)
        {
            set = new List<ISearchCriterion>(list);
        }

        public virtual void Add(ISearchCriterion criterion)
        {
            set.Add(criterion);
        }

        public virtual bool Contains(string identifier)
        {
            return set.Any(sc => sc.Identifier == identifier);
        }

        public IEnumerator<ISearchCriterion> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        public virtual int GetPosition(string identifier)
        {
            return set.IndexOf(set.FirstOrDefault(c => c.Identifier == identifier));
        }

        public virtual void Insert(int pos, ISearchCriterion criterion)
        {
            set.Insert(pos, criterion);
        }

        public virtual bool Remove(string identifier)
        {
            return set.Remove(set.FirstOrDefault(c => c.Identifier == identifier));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual ISearchCriterion GetCriterion(string identifier)
        {
            return set.FirstOrDefault(c => c.Identifier == identifier);
        }
    }
}
