using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Web.Controllers.Xml
{
    public class XmlNamespaceTypedCriterion<T> : TypedCriterion<T>, IQualifiedSearchCriterion where T : IComparable<T>, IConvertible
    {
        

        private readonly XmlQualifiedName xqname;

        public XmlNamespaceTypedCriterion(TypedCriterion<T> criterion, XmlQualifiedName xqname): base(criterion)
        {
            this.xqname = xqname;
        }

        public XmlNamespaceTypedCriterion(string name, string ns, string title): base( $"{{{ns}}}{name}")
        {
            this.xqname = new XmlQualifiedName(name, ns);
            Title = title;
        }

        public XmlNamespaceTypedCriterion(XmlQualifiedName xqname, string title): base($"{{{xqname.Namespace}}}{xqname.Name}")
        {
            this.xqname = xqname;
            Title = title;
        }

        public string Namespace => xqname.Namespace;

        public string Name => xqname.Name;

        

        
    }
}