using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Terradue.Search.Model
{
    /// <summary>
    /// Interface from class implementing Search Broker
    /// </summary>
    public interface ISearchBroker<TSearchable> 
        where TSearchable: ISearchable
    {
        Task<ISearchEngine> GetSearchEngine();

        
    }
}
