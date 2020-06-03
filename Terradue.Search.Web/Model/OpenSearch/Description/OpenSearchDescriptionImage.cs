using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System;

namespace Terradue.Search.Web.Model.OpenSearch.Description
{
    /// <remarks/>
    [DataContract(Name = "Image", Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    public class OpenSearchDescriptionImage
    {

        private int heightField;

        private int widthField;

        private string typeField1;

        private string valueField1;

        /// <remarks/>
        [XmlAttribute]
        [DataMember]
        public int height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        [DataMember]
        public int width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        [DataMember]
        public string type
        {
            get
            {
                return this.typeField1;
            }
            set
            {
                this.typeField1 = value;
            }
        }

        /// <remarks/>
        [XmlText]
        [DataMember]
        public string Value
        {
            get
            {
                return this.valueField1;
            }
            set
            {
                this.valueField1 = value;
            }
        }
    }

}
