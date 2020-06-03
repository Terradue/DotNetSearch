using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Model
{
    public interface ISearchQuery
    {
        ISearchParameterSet Parameters { get; }
    }
}