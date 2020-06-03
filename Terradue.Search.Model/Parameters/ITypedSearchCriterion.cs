using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terradue.Search.Model.Implementation;

namespace Terradue.Search.Model.Parameters
{
    public interface ITypedSearchCriterion<T> : ISearchCriterion where T : IComparable<T>
    {
        Range<T> ValueRange { get; }

    }
}