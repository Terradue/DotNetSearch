using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Web.Controllers.Xml
{
    internal interface IQualifiedSearchCriterion : ISearchCriterion
    {
        string Namespace { get; }

        string Name { get; }

    }
}