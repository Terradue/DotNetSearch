using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System;

namespace Terradue.Search.Web.Model.OpenSearch.Description
{
    /// <remarks/>
    [DataContract(Name = "Query", Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    public class OpenSearchDescriptionQuery
    {

        private string roleField;

        private string searchTermsField;

        private string valueField2;

        /// <remarks/>
        [XmlAttribute]
        [DataMember]
        public string role
        {
            get
            {
                return this.roleField;
            }
            set
            {
                this.roleField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        [DataMember]
        public string searchTerms
        {
            get
            {
                return this.searchTermsField;
            }
            set
            {
                this.searchTermsField = value;
            }
        }

        /// <remarks/>
        [XmlText]
        [DataMember]
        public string Value
        {
            get
            {
                return this.valueField2;
            }
            set
            {
                this.valueField2 = value;
            }
        }
    }

}
