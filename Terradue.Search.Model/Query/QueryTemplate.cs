using System.Collections;
using System.Collections.Generic;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Model.Query
{
    public class QueryTemplate : IQueryTemplate
    {
        private IDictionary<string, ISearchParameterSet> parametersTemplates;

        public QueryTemplate(IDictionary<string, ISearchParameterSet> parametersTemplates)
        {
            this.parametersTemplates = parametersTemplates;
        }

        public QueryTemplate()
        {
            this.parametersTemplates = new Dictionary<string, ISearchParameterSet>();
        }

        public IDictionary<string, ISearchParameterSet> ParametersTemplates => parametersTemplates;
    }
}