using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using Terradue.Search.Engines.Parameters.Common;
using Terradue.Search.Model;
using Terradue.Search.Model.Parameters;
using Terradue.Search.Model.Query;
using Terradue.Search.Web.Controllers.Xml;
using Terradue.Search.Web.Model.OpenSearch.Description;

namespace Terradue.Search.Web.Controllers.OpenSearch
{
    public static class OpenSearchHelpers
    {
        public static readonly string UKNS = "http://www.terradue.com/unknown";
        public static readonly string OSNS = "http://a9.com/-/spec/opensearch/1.1/";
        public static readonly string OSPARAMNS = "http://a9.com/-/spec/opensearch/extensions/parameters/1.0/";
        public static readonly XmlQualifiedName UKNS_PREFIX = new XmlQualifiedName("no", UKNS);
        public static readonly XmlQualifiedName OSNS_PREFIX = new XmlQualifiedName("os", OSNS);
        public static readonly XmlQualifiedName OSPARAMNS_PREFIX = new XmlQualifiedName("param", OSPARAMNS);
        public static readonly XmlQualifiedName OSXN_SEARCHTERMS = new XmlQualifiedName("searchTerms", OSNS);
        public static readonly XmlQualifiedName OSXN_STARTPAGE = new XmlQualifiedName("startPage", OSNS);
        public static readonly XmlQualifiedName OSXN_STARTINDEX = new XmlQualifiedName("startIndex", OSNS);
        public static readonly XmlQualifiedName OSXN_COUNT = new XmlQualifiedName("count", OSNS);
        public static readonly XmlQualifiedName OSXN_LANGUAGE = new XmlQualifiedName("language", OSNS);
        public static XmlSerializerNamespaces BASENS = new XmlSerializerNamespaces(new XmlQualifiedName[]{
            OSNS_PREFIX, OSPARAMNS_PREFIX
        });
        private static Regex qualifiedIdRegex = new Regex(@"^{(?'namespace'[^}]*)}(?'name'.+)$");

        public static XmlSerializerNamespaces GetExtraNamespacesIfAny(this XmlNamespaceSearchCriterionSet criterionSet)
        {
            if (criterionSet.NamespacePrefixes == null || criterionSet.NamespacePrefixes.Count() == 0) return null;
            return new XmlSerializerNamespaces(
                criterionSet.NamespacePrefixes.ToArray()
            );
        }

        internal static XmlNamespaceSearchCriterionSet QualifyCriterionSetForOpenSearch(ISearchCriterionSet criterionSet)
        {
            if (criterionSet is XmlNamespaceSearchCriterionSet) return criterionSet as XmlNamespaceSearchCriterionSet;

            XmlNamespaceSearchCriterionSet xmlCriterionSet = new XmlNamespaceSearchCriterionSet();

            foreach (var criterion in criterionSet)
            {
                ISearchCriterion newCriterion = criterion;
                if (criterion is FreeTextSearchCriterion)
                {
                    xmlCriterionSet.AddNamespacePrefixIfNotExist(OSNS_PREFIX);
                    xmlCriterionSet.Add(XmlNamespaceSearchCriterionSet.QualifyCriterion(OSXN_SEARCHTERMS, criterion));
                }
                else if (criterion is PageSizeSearchCriterion)
                {
                    xmlCriterionSet.AddNamespacePrefixIfNotExist(OSNS_PREFIX);
                    xmlCriterionSet.Add(XmlNamespaceSearchCriterionSet.QualifyCriterion(OSXN_COUNT, criterion));
                }
                else if (criterion is StartIndexSearchCriterion)
                {
                    xmlCriterionSet.AddNamespacePrefixIfNotExist(OSNS_PREFIX);
                    xmlCriterionSet.Add(XmlNamespaceSearchCriterionSet.QualifyCriterion(OSXN_STARTINDEX, criterion));
                }
                else if (criterion is StartPageSearchCriterion)
                {
                    xmlCriterionSet.AddNamespacePrefixIfNotExist(OSNS_PREFIX);
                    xmlCriterionSet.Add(XmlNamespaceSearchCriterionSet.QualifyCriterion(OSXN_STARTPAGE, criterion));
                }
                else if (criterion is LanguageSearchCriterion)
                {
                    xmlCriterionSet.AddNamespacePrefixIfNotExist(OSNS_PREFIX);
                    xmlCriterionSet.Add(XmlNamespaceSearchCriterionSet.QualifyCriterion(OSXN_LANGUAGE, criterion));
                }
                else
                {
                    xmlCriterionSet.AddNamespacePrefixIfNotExist(UKNS_PREFIX);
                    newCriterion = XmlNamespaceSearchCriterionSet.QualifyCriterion(new XmlQualifiedName(criterion.Identifier, UKNS), criterion);
                }
            }
            return xmlCriterionSet;
        }

        public static OpenSearchDescription GenerateOpenSearchDescription(ISearchEngine searchEngine, HttpContext httpContext, IOpenSearchContext openSearchContext)
        {
            OpenSearchDescription openSearchDescription = new OpenSearchDescription();

            openSearchDescription.Url = OpenSearchHelpers.CreateOpenSearchDescriptionUrls(searchEngine.GetSearchFunctions(), httpContext, openSearchContext).ToList();

            return openSearchDescription;
        }

        /// <summary>
        /// This function creates a list of opensearch Url with templates from a list
        /// of search functions based on the formatting capabities of the httpContext
        /// </summary>
        /// <param name="searchFunctions">list of search functions to make an opensearch Url template from</param>
        /// <param name="httpContext">the HTTP context</param>
        /// <returns></returns>
        public static IEnumerable<OpenSearchDescriptionUrl> CreateOpenSearchDescriptionUrls(IDictionary<string, ISearchFunction> searchFunctions, HttpContext httpContext, IOpenSearchContext openSearchContext)
        {
            // prepare an empty url list
            List<OpenSearchDescriptionUrl> urls = new List<OpenSearchDescriptionUrl>();

            // get the OpenSearch Formatter selector
            OpenSearchFormatterSelector selector = (OpenSearchFormatterSelector)httpContext.RequestServices.GetService(typeof(OpenSearchFormatterSelector));

            // urls are the combination of the search function possible results type and the relevant formatters
            foreach (var searchFunction in searchFunctions)
            {
                foreach (var formatter in selector.SelectFormatters(searchFunction.Value.ResultType))
                {
                    // if the formatter is able to serialize the result, then there is a possible url template
                    var supportedContentTypes = formatter.GetSupportedContentTypes(null, searchFunction.Value.ResultType);
                    if (supportedContentTypes == null || supportedContentTypes.Count == 0) continue;
                    var xmlCriterionSet = QualifyCriterionSetForOpenSearch(searchFunction.Value.SearchCriterionSet);
                    OpenSearchDescriptionUrl url = new OpenSearchDescriptionUrl(
                        supportedContentTypes.First(),
                        CreateQueryStringUrlTemplate(searchFunction.Key, xmlCriterionSet, httpContext, openSearchContext),
                        "results",
                        xmlCriterionSet.GetExtraNamespacesIfAny()
                    );
                    urls.Add(url);
                }
            }

            return urls;
        }

        private static string CreateQueryStringUrlTemplate(string functionId, XmlNamespaceSearchCriterionSet xmlCriterionSet, HttpContext httpContext, IOpenSearchContext openSearchContext)
        {
            UriBuilder uriTemplate = new UriBuilder(openSearchContext.BaseSearchUrlTemplate.ToString().Replace("{functionId}", functionId));

            uriTemplate.Query = xmlCriterionSet.ToQueryStringUrlTemplate();

            return uriTemplate.ToString();
        }

        public static ISearchQuery CreateSearchQuery(IQueryCollection queryCollection, ISearchFunction searchFunction)
        {
            List<SearchParameterSet> sets = new List<SearchParameterSet>();

            List<ISearchParameter> parameters = new List<ISearchParameter>();
            foreach (var kvp in queryCollection)
            {
                foreach (var value in kvp.Value)
                {
                    if (string.IsNullOrEmpty(value)) continue;
                    ISearchParameter parameter = CreateParameter(kvp.Key, kvp.Value, searchFunction.SearchCriterionSet);
                    if (parameter != null)
                        parameters.Add(parameter);
                }
            }
            SearchParameterSet set = new SearchParameterSet(parameters);
            sets.Add(set);

            return new SearchQuery(sets.LastOrDefault());
        }

        private static ISearchParameter CreateParameter(string identifier, string value, ISearchCriterionSet criterionSet)
        {
            ISearchCriterion criterion = criterionSet.GetCriterion(identifier);
            if (criterion == null) return null;
            return criterion.CreateParameter(value);
        }

        public static string ToFullyQualifiedNameString(this XmlQualifiedName xqname)
        {
            return string.Format("{{{0}}}{1}", xqname.Namespace, xqname.Name);
        }

        public static XmlQualifiedName ToXmlQualifiedName(this string identifier)
        {
            var match = qualifiedIdRegex.Match(identifier);
            if (!match.Success)
                return null;
            return new XmlQualifiedName(match.Groups["name"].Value, match.Groups["namespace"].Value);
        }
    }
}
