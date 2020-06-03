using System;
using System.Collections.Generic;
using System.Linq;
using Terradue.Search.Engines.Simple;
using Terradue.Search.Model.Parameters;
using Terradue.Search.Model.Query;
using Xunit;

namespace Terradue.Search.Tests.Parameters
{
    public class ParametersTests
    {
        [Fact]
        public void ParameterCreationTest()
        {
            TypedCriterion<double> criterion = new TypedCriterion<double>("{{test}parameter");
            criterion.ValuePattern = new System.Text.RegularExpressions.Regex(@"^\d*.\d{2}$");
            criterion.ValueRange = new Model.Implementation.Range<double>(10.0, 20.0);

            ISearchParameter parameter = criterion.CreateParameter("0");
            Assert.IsAssignableFrom<WrongSearchParameter>(parameter);
            Assert.Contains("regular expression", ((WrongSearchParameter)parameter).ErrorMessage);

        }

    }
}
