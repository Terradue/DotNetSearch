using Terradue.Search.Model.Parameters;

namespace Terradue.Search.Model.Parameters
{
    public class WrongSearchParameter : ISearchParameter
    {
        private readonly string identifier;
        private readonly string value;
        private readonly string errorMessage;

        public WrongSearchParameter(string identifier, string value, string errorMessage)
        {
            this.identifier = identifier;
            this.value = value;
            this.errorMessage = errorMessage;
        }

        public string Identifier => identifier;

        public object Value => value;

        public string ErrorMessage => errorMessage;
    }
}