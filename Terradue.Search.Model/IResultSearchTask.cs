using System;
using System.Threading.Tasks;

namespace Terradue.Search.Model
{
    public interface IResultSearchTask: ISearchTask
    {
        Task<object> SearchResult();

    }
}