using System.Collections.Generic;
using System.Collections;
using Terradue.Search.Model.Parameters;
using System.Xml;
using System.Linq;
using System;
using System.Text.RegularExpressions;
using Terradue.Search.Web.Controllers.OpenSearch;

namespace Terradue.Search.Web.Controllers.Xml
{
    public class XmlNamespaceSearchCriterionSet : SearchCriterionSet
    {

        private readonly List<XmlQualifiedName> namespacePrefixes;

        public IEnumerable<XmlQualifiedName> NamespacePrefixes => namespacePrefixes;

        public XmlNamespaceSearchCriterionSet() : base(new List<ISearchCriterion>())
        {
            this.namespacePrefixes = new List<XmlQualifiedName>();
        }

        public XmlNamespaceSearchCriterionSet(IEnumerable<ISearchCriterion> list, XmlQualifiedName[] namespacePrefixes) : base(new List<ISearchCriterion>())
        {
            this.namespacePrefixes = new List<XmlQualifiedName>(namespacePrefixes);
            foreach (var criterion in list)
            {
                Add(criterion);
            }
        }

        public override void Add(ISearchCriterion criterion)
        {
            IQualifiedSearchCriterion qcriterion = null;
            if (criterion is IQualifiedSearchCriterion)
                qcriterion = criterion as IQualifiedSearchCriterion;
            else
                qcriterion = QualifyCriterion(criterion);

            AddCriterionNamespaceIfNotExist(qcriterion);

            base.Add(criterion);
        }

        public override bool Remove(string identifier)
        {
            if (Remove(identifier))
            {
                XmlQualifiedName xqname = identifier.ToXmlQualifiedName();
                if (xqname != null)
                    RemoveCriterionNameSpaceIfNone(xqname.Namespace);
                return true;
            }
            return false;
        }

        private void RemoveCriterionNameSpaceIfNone(string ns)
        {
            if (this.GetQualifiedCriteria().Any(c => c.Namespace == ns))
                return;
            namespacePrefixes.Remove(namespacePrefixes.FirstOrDefault(nsp => nsp.Namespace == ns));
        }

        public bool Remove(string name, string ns)
        {
            return Remove($"{{{ns}}}{name}");
        }

        internal static IQualifiedSearchCriterion QualifyCriterion(ISearchCriterion criterion)
        {
            XmlQualifiedName xqname = criterion.Identifier.ToXmlQualifiedName();
            if (xqname == null)
                throw new ArgumentException("Criterion '{0}' must declare namespace to be qualified", criterion.Identifier);

            return QualifyCriterion(xqname, criterion);
        }

        internal static IQualifiedSearchCriterion QualifyCriterion(XmlQualifiedName xmlQualifiedName, ISearchCriterion criterion)
        {
            Type typedCriterion = typeof(TypedCriterion<>);
            Type type = GetInstanceOfGenericType(typedCriterion, criterion);
            if (type == null)
                throw new ArgumentException("Criterion '{0}' must be typed to be qualified", criterion.Identifier);

            Type[] types = type.GetGenericArguments();

            Type xmlCriterion = typeof(XmlNamespaceTypedCriterion<>);
            Type construct = xmlCriterion.MakeGenericType(types);

            return (IQualifiedSearchCriterion)Activator.CreateInstance(construct, new object[] { criterion, xmlQualifiedName });
        }

        internal void AddNamespacePrefixIfNotExist(XmlQualifiedName namespacePrefix)
        {
            if (namespacePrefixes.Any(prefix => prefix.Namespace == namespacePrefix.Namespace)) return;
            namespacePrefixes.Add(namespacePrefix);
        }

        static Type GetInstanceOfGenericType(Type genericType, object instance)
        {
            Type type = instance.GetType();
            while (type != null)
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == genericType)
                {
                    return type;
                }
                type = type.BaseType;
            }
            return null;
        }

        private void AddCriterionNamespaceIfNotExist(IQualifiedSearchCriterion qcriterion)
        {
            if (!namespacePrefixes.Any(prefix => prefix.Namespace == qcriterion.Namespace))
            {
                for (int i = 0; i < namespacePrefixes.Count; i++)
                {
                    if (namespacePrefixes.Any(prefix => prefix.Namespace == "ns" + i)) continue;
                    namespacePrefixes.Add(new XmlQualifiedName("ns" + i, qcriterion.Namespace));
                }
            }
        }

        internal IQualifiedSearchCriterion GetQualifiedCriterion(XmlQualifiedName xqname)
        {
            return GetQualifiedCriteria().FirstOrDefault(xmlCriterion => xmlCriterion.Name == xqname.Name && xmlCriterion.Namespace == xqname.Namespace);
        }

        internal IEnumerable<IQualifiedSearchCriterion> GetQualifiedCriteria()
        {
            return this.OfType<IQualifiedSearchCriterion>();
        }

        public string ToQueryStringUrlTemplate()
        {
            return string.Join("&", GetQualifiedCriteria().Select(c =>
            {
                string prefix = GetPrefixForCriterion(c);
                if (prefix != null)
                    prefix += ":";
                return string.Format("{3}={{{0}{1}{2}}}", prefix, c.Name, c.Mandatory ? "" : "?",
                    c.Identifier);
            }));
        }

        internal string GetPrefixForCriterion(IQualifiedSearchCriterion c)
        {
            var nsPrefix = namespacePrefixes.FirstOrDefault(nsp => nsp.Namespace == c.Namespace);
            if (nsPrefix == null)
                return null;
            return nsPrefix.Name;
        }
    }
}