using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Terradue.Search.Web.Model.OpenSearch.Description;

namespace Terradue.Search.Web.Formatters.OpenSearch
{
    public class OpenSearchDescriptionFormatter : XmlSerializerOutputFormatter
    {
        public OpenSearchDescriptionFormatter()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/opensearchdescription+xml"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        /// <inheritdoc />
        protected override bool CanWriteType(Type type)
        {
            if (typeof(OpenSearchDescription).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

    }
}
