using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Terradue.Search.Model;

namespace Terradue.Search.Engines.Simple
{
    public class EnumerableResultSearchTask<TResult> : ResultSearchTask<IEnumerable<TResult>>, ISearchTask
    {

        public EnumerableResultSearchTask(Task<IEnumerable<TResult>> searchTask) : base(searchTask)
        {
        }

        public Task<IEnumerable<TResult>> SearchResults<TSearchResult>() where TSearchResult : TResult
        {
            return resultTask;
        }

    }
}