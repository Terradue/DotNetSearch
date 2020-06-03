using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Terradue.Search.Model.Implementation;

namespace Terradue.Search.Model.Parameters
{
    public class TypedCriterion<T> : ITypedSearchCriterion<T>
        where T : IComparable<T>, IConvertible
    {
        private readonly string identifier;
        protected IEnumerable<TypedParameter<T>> options;
        protected Range<T> valueRange;
        protected bool mandatory;
        protected string title;
        protected Regex valuePattern;

        public TypedCriterion(TypedCriterion<T> criterion) : this(criterion.identifier)
        {
            this.options = criterion.options;
            this.valueRange = criterion.valueRange;
            this.mandatory = criterion.mandatory;
            this.title = criterion.title;
            this.valuePattern = criterion.valuePattern;
        }

        public TypedCriterion(string identifier)
        {
            this.identifier = identifier;
        }

        public string Identifier => identifier;

        public bool Mandatory
        {
            get
            {
                return mandatory;
            }

            set
            {
                mandatory = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public Regex ValuePattern
        {
            get
            {
                return valuePattern;
            }

            set
            {
                valuePattern = value;
            }
        }

        public Range<T> ValueRange
        {
            get
            {
                return valueRange;
            }

            set
            {
                valueRange = value;
            }
        }

        public IEnumerable<ISearchParameter> Options
        {
            get
            {
                return (IEnumerable<ISearchParameter>)options;
            }

            internal set
            {
                options = (IEnumerable<TypedParameter<T>>)value;
            }
        }

        public Type GetValueType()
        {
            return typeof(T);
        }

        public ISearchParameter CreateParameter(string value)
        {
            if (ValuePattern != null && !ValuePattern.IsMatch(value))
                return new WrongSearchParameter(Identifier, value, string.Format("value does not match regular expression '{0}'", ValuePattern.ToString()));
            T tvalue;
            try
            {
                tvalue = (T)Convert.ChangeType(value, Type.GetTypeCode(typeof(T)));
            }
            catch (FormatException){
                return new WrongSearchParameter(Identifier, value, string.Format("value cannot be parsed as '{0}'", Type.GetTypeCode(typeof(T))));
            }

            if (ValueRange != null && !ValueRange.ContainsValue(tvalue))
                return new WrongSearchParameter(Identifier, value, string.Format("value in not within range '{0}'", ValueRange));

            return new TypedParameter<T>(Identifier, tvalue);
        }
    }
}