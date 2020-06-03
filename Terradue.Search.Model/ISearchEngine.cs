using System.Collections.Generic;
using System.Threading.Tasks;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSearchResult"></typeparam>
    public interface ISearchEngine
    {
        ISearchTask CreateSearch(string queryString);

        IDictionary<string, ISearchFunction> GetSearchFunctions();

    }
}