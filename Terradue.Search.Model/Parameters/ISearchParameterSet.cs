using System;
using System.Collections.Generic;

namespace Terradue.Search.Model.Parameters
{
    public interface ISearchParameterSet : IEnumerable<ISearchParameter>
    {
        bool Contains(string identifier);

        bool Remove(string identifier);

        void Add(ISearchParameter parameter);

        T GetValueOr<T>(string identifier, T defaultValue);
    }
}
