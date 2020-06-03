using System;
using System.Threading;
using System.Threading.Tasks;

namespace Terradue.Search.Model
{
    public abstract class ResultSearchTask<TSearchResult> : ISearchTask, IResultSearchTask
    {
        protected Task<TSearchResult> resultTask;

        public ResultSearchTask(Task<TSearchResult> searchTask)
        {
            this.resultTask = searchTask;
        }

        public Task Search()
        {
            return resultTask;
        }

        public Task<TSearchResult> SearchResult<TResult>() where TResult: TSearchResult
        {
            return resultTask;
        }

        public async Task<object> SearchResult()
        {
            await resultTask.ConfigureAwait(false);
            return (object)resultTask.Result;
        }
    }
}