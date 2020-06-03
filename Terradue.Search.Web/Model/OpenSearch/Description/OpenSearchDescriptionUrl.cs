using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System;

namespace Terradue.Search.Web.Model.OpenSearch.Description
{
    /// <remarks/>
    [DataContract(Name = "Url", Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    public partial class OpenSearchDescriptionUrl
    {

        private string typeField;

        private string templateField;

        private string relField;

        private int pageOffset = 1;

        private int indexOffset = 1;

        public OpenSearchDescriptionUrl()
        {

        }

        public OpenSearchDescriptionUrl(string type, string template, string rel, XmlSerializerNamespaces extraNamespace)
        {
            this.Relation = rel;
            ExtraNamespace = extraNamespace;
            this.Template = template;
            this.Type = type;
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "type")]
        [DataMember]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "template")]
        [DataMember]
        public string Template
        {
            get
            {
                return this.templateField;
            }
            set
            {
                this.templateField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "rel")]
        [DataMember]
        public string Relation
        {
            get
            {
                return this.relField;
            }
            set
            {
                this.relField = value;
            }
        }

        [XmlAttribute(AttributeName = "pageOffset")]
        [DataMember]
        public int PageOffset
        {
            get
            {
                return pageOffset;
            }
            set
            {
                pageOffset = value;
            }
        }

        [XmlAttribute(AttributeName = "indexOffset")]
        [DataMember]
        public int IndexOffset
        {
            get
            {
                return indexOffset;
            }
            set
            {
                indexOffset = value;
            }
        }

        XmlSerializerNamespaces extraNamespace = new XmlSerializerNamespaces();
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces ExtraNamespace
        {
            get
            {
                return extraNamespace;
            }
            set
            {
                extraNamespace = value;
            }
        }

    }

}
