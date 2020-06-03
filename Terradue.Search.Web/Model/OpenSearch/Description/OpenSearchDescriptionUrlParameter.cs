using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using System;

namespace Terradue.Search.Web.Model.OpenSearch.Description
{
    /// <remarks/>
    [SerializableAttribute()]
    [XmlTypeAttribute("Parameter", Namespace = "http://a9.com/-/spec/opensearch/extensions/parameters/1.0/")]
    public partial class OpenSearchDescriptionUrlParameter
    {

        private string nameField;

        private string valueField;

        private string minimumField = null;

        private string maximumField = null;

        private string pattern = null;

        private string title = null;

        private string minExclusive = null;

        private string maxExclusive = null;

        private string minInclusive = null;

        private string maxInclusive = null;

        private string step = null;

        private List<OpenSearchDescriptionUrlParameterOption> options = new List<OpenSearchDescriptionUrlParameterOption>();

        /// <remarks/>
        [XmlAttribute(AttributeName = "name")]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "value")]
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
        [XmlAttribute(AttributeName = "minimum")]
        public string Minimum
        {
            get
            {
                return this.minimumField;
            }
            set
            {
                this.minimumField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "maximum")]
        public string Maximum
        {
            get
            {
                return this.maximumField;
            }
            set
            {
                this.maximumField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "pattern")]
        public string Pattern
        {
            get
            {
                return this.pattern;
            }
            set
            {
                this.pattern = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "title")]
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "minExclusive")]
        public string MinExclusive
        {
            get
            {
                return this.minExclusive;
            }
            set
            {
                this.minExclusive = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "maxExclusive")]
        public string MaxExclusive
        {
            get
            {
                return this.maxExclusive;
            }
            set
            {
                this.maxExclusive = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "minInclusive")]
        public string MinInclusive
        {
            get
            {
                return this.minInclusive;
            }
            set
            {
                this.minInclusive = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "maxInclusive")]
        public string MaxInclusive
        {
            get
            {
                return this.maxInclusive;
            }
            set
            {
                this.maxInclusive = value;
            }
        }

        /// <remarks/>
        [XmlAttribute(AttributeName = "step")]
        public string Step
        {
            get
            {
                return this.step;
            }
            set
            {
                this.step = value;
            }
        }

        [XmlElement("Option")]
        public List<OpenSearchDescriptionUrlParameterOption> Options
        {
            get
            {
                return options;
            }
            set
            {
                options = value;
            }
        }

        [XmlAnyElement]
        public XmlElement[] Any
        {
            get;
            set;
        }
        
    }

}
