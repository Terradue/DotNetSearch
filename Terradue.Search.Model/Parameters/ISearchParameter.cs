using System;

namespace Terradue.Search.Model.Parameters
{
    public interface ISearchParameter
    {
        string Identifier { get; }
        
        object Value { get; }
    }
}