using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terradue.Search.Model.Implementation;

namespace Terradue.Search.Model.Parameters
{
    public interface ISearchCriterion
    {
        string Identifier { get; }

        bool Mandatory { get; }

        string Title { get; }

        Regex ValuePattern { get; }

        Type GetValueType();

        ISearchParameter CreateParameter(string value);

    }
}