using System;
using System.Collections.Specialized;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using Terradue.Search.Web.Controllers.OpenSearch;

namespace Terradue.Search.Web.Model.OpenSearch.Description
{
    public partial class OpenSearchDescription
    {
        OpenSearchDescriptionUrl defaultUrl = null;

        public OpenSearchDescription()
        {
            ExtraNamespace = OpenSearchHelpers.BASENS;
           
            Url = new List<OpenSearchDescriptionUrl>();
        }

        public void AddUrl(OpenSearchDescriptionUrl openSearchDescriptionUrl)
        {
            if (Url.Any(u =>
            {
                var mimeType1 = new System.Net.Mime.ContentType(u.Type);
                var mimeType2 = new System.Net.Mime.ContentType(openSearchDescriptionUrl.Type);
                return u.Template.Equals(openSearchDescriptionUrl.Template) && mimeType1.Equals(mimeType2);
            }))
                this.Url.Add(openSearchDescriptionUrl);
        }

        [System.Xml.Serialization.XmlIgnore]
        public OpenSearchDescriptionUrl DefaultUrl
        {
            get
            {
                if (defaultUrl == null && Url != null && Url.Count() > 0)
                    defaultUrl = Url.FirstOrDefault(u => u.Type == "application/atom+xml");
                if (defaultUrl == null && Url != null && Url.Count() > 0)
                    defaultUrl = Url.FirstOrDefault(u => u.Type == "application/json");
                if (defaultUrl == null && Url != null && Url.Count() > 0)
                    defaultUrl = Url.FirstOrDefault(u => u.Type == "application/xml");
                if (defaultUrl == null && Url != null && Url.Count() > 0)
                    return Url.First();
                return defaultUrl;
            }
            set
            {
                defaultUrl = value;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public string[] ContentTypes
        {
            get
            {
                return Url.Select<OpenSearchDescriptionUrl, string>(u => u.Type).ToArray();
            }
        }

    }
}

