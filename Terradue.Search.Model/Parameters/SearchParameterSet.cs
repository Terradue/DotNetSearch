using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Terradue.Search.Model.Parameters
{
    public class SearchParameterSet : ISearchParameterSet
    {
        private readonly List<ISearchParameter> set;

        public SearchParameterSet() : this(new List<ISearchParameter>())
        {
        }

        public SearchParameterSet(IEnumerable<ISearchParameter> list)
        {
            set = new List<ISearchParameter>(list);
        }

        public virtual void Add(ISearchParameter parameter)
        {
            set.Add(parameter);
        }

        public virtual bool Contains(string identifier)
        {
            return set.Any(sc => sc.Identifier == identifier);
        }

        public IEnumerator<ISearchParameter> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        public virtual int GetPosition(string identifier)
        {
            return set.IndexOf(set.FirstOrDefault(c => c.Identifier == identifier));
        }

        public T GetValueOr<T>(string identifier, T defaultValue)
        {
            var parameter = set.FirstOrDefault(c => c.Identifier == identifier);
            if (parameter == null) return defaultValue;
            return (T)parameter.Value;
        }

        public virtual void Insert(int pos, ISearchParameter parameter)
        {
            set.Insert(pos, parameter);
        }

        public virtual bool Remove(string identifier)
        {
            return set.Remove(set.FirstOrDefault(c => c.Identifier == identifier));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
