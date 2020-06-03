using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System;

namespace Terradue.Search.Web.Model.OpenSearch.Description
{
    /// <remarks/>
    [DataContract(Name = "Parameter", Namespace = "http://a9.com/-/spec/opensearch/extensions/parameters/1.0/")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://a9.com/-/spec/opensearch/extensions/parameters/1.0/")]
    public partial class OpenSearchDescriptionUrlParameterOption
    {

        private string valueField;

        private string labelField;

        /// <remarks/>
        [XmlAttribute(AttributeName = "value")]
        [DataMember]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "label")]
        [DataMember]
        public string Label
        {
            get
            {
                return this.labelField;
            }
            set
            {
                this.labelField = value;
            }
        }
    }

}
