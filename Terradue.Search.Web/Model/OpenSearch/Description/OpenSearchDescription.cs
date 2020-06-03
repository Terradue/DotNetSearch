using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Terradue.Search.Web.Model.OpenSearch.Description
{

    /// <remarks/>
    [SerializableAttribute()]
    [XmlTypeAttribute(Namespace = "http://a9.com/-/spec/opensearch/1.1/")]
    [XmlRootAttribute(Namespace = "http://a9.com/-/spec/opensearch/1.1/", IsNullable = false)]
    public partial class OpenSearchDescription
    {

        const int ShortNameMaxLength = 16;

        private string shortNameField;

        private string descriptionField;

        private string tagsField;

        private string contactField;

        private List<OpenSearchDescriptionUrl> urlField;

        private string longNameField;

        private List<OpenSearchDescriptionImage> imageField;

        private OpenSearchDescriptionQuery queryField;

        private string developerField;

        private string attributionField;

        private string syndicationRightField;

        private string adultContentField;

        private string languageField;

        private string outputEncodingField;

        private string inputEncodingField;

        /// <remarks/>
        [DataMember]
        public string ShortName
        {
            get
            {
                return this.shortNameField;
            }
            set
            {

                if (value != null && value.Length > ShortNameMaxLength)
                    this.shortNameField = value.Substring(0, ShortNameMaxLength);
                else
                    this.shortNameField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Tags
        {
            get
            {
                return this.tagsField;
            }
            set
            {
                this.tagsField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Url")]
        [DataMember]
        public List<OpenSearchDescriptionUrl> Url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string LongName
        {
            get
            {
                return this.longNameField;
            }
            set
            {
                this.longNameField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("Image")]
        [DataMember]
        public List<OpenSearchDescriptionImage> Image
        {
            get
            {
                return this.imageField;
            }
            set
            {
                this.imageField = value;
            }
        }

        /// <remarks/> 
        [DataMember]
        public OpenSearchDescriptionQuery Query
        {
            get
            {
                return this.queryField;
            }
            set
            {
                this.queryField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Developer
        {
            get
            {
                return this.developerField;
            }
            set
            {
                this.developerField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Attribution
        {
            get
            {
                return this.attributionField;
            }
            set
            {
                this.attributionField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string SyndicationRight
        {
            get
            {
                return this.syndicationRightField;
            }
            set
            {
                this.syndicationRightField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string AdultContent
        {
            get
            {
                return this.adultContentField;
            }
            set
            {
                this.adultContentField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string Language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string OutputEncoding
        {
            get
            {
                return this.outputEncodingField;
            }
            set
            {
                this.outputEncodingField = value;
            }
        }

        /// <remarks/>
        [DataMember]
        public string InputEncoding
        {
            get
            {
                return this.inputEncodingField;
            }
            set
            {
                this.inputEncodingField = value;
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


        public OpenSearchDescriptionUrl GetUrlByType(string type)
        {
            foreach (OpenSearchDescriptionUrl url in urlField)
            {
                if (url.Type == type)
                    return url;
            }

            return null;
        }

    }

}
