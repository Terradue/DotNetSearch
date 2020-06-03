using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terradue.Search.Engines.Parameters.Common;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Engines.Simple
{
    public class EnumerableSearchEngine<T> : BaseSearchEngine
    {
        private IEnumerable<T> enumerable;

        public EnumerableSearchEngine(IEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }

        public override ISearchTask CreateSearch(string queryString)
        {
            return (ISearchTask)SearchMany(queryString);
        }

        public override ISearchTask CreateSearch(ISearchQuery query)
        {
            return base.CreateSearch(query);
        }

        public EnumerableResultSearchTask<T> SearchMany(string queryString)
        {
            return new EnumerableResultSearchTask<T>(Task<T>.Run(() =>
                enumerable.Where(o => string.IsNullOrEmpty(queryString) ? true : o.ToString().Contains(queryString))
            ));
        }

    }
}
