using System;
using Terradue.Search.Model;

namespace Terradue.Search.Model.Parameters
{
    public class TypedParameter<T> : ISearchParameter where T : IComparable<T>
    {
        private readonly T value;
        private string identifier;

        public TypedParameter(string identifier, T value)
        {
            this.identifier = identifier;
            this.value = value;
        }

        public string Identifier { get => identifier; set => identifier = value; }
        public object Value => value;

        public T GetValueOr(T defaultValue)
        {
            return value is T ? (T)value : defaultValue;
        }
    }
}