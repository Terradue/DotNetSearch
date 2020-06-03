using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;

namespace Terradue.Search.Web.Model.OpenSearch.Description
{

    /// <remarks/>
    public partial class OpenSearchDescriptionUrl
    {

        private string methodField;

        private string enctypeField;

        private OpenSearchDescriptionUrlParameter[] parameterField;

        /// <remarks/>
        [XmlAttribute(AttributeName = "method", Namespace = "http://a9.com/-/spec/opensearch/extensions/parameters/1.0/")]
        [DataMember]
        public string ParametersMethod
        {
            get
            {
                return this.methodField;
            }
            set
            {
                this.methodField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "enctype", Namespace = "http://a9.com/-/spec/opensearch/extensions/parameters/1.0/")]
        [DataMember]
        public string ParametersEncodingType
        {
            get
            {
                return this.enctypeField;
            }
            set
            {
                this.enctypeField = value;
            }
        }

        /// <remarks/>
        [XmlElement("Parameter", Namespace = "http://a9.com/-/spec/opensearch/extensions/parameters/1.0/")]
        public OpenSearchDescriptionUrlParameter[] Parameters
        {
            get
            {
                return this.parameterField;
            }
            set
            {
                this.parameterField = value;
            }
        }
    }

}
