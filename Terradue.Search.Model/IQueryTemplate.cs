using System.Collections.Generic;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Model
{
    public interface IQueryTemplate
    {
        IDictionary<string, ISearchParameterSet> ParametersTemplates { get; }
    }
}