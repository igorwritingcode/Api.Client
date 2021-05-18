using System.Collections.Generic;

namespace Api.Client.Generator.Model
{
    //Represents resource in client API model
    public class ApiResource
    {
        private readonly string _name;
        public SortedDictionary<string, ApiRequest> Requests { get; init; }

        public ApiResource(
            string name,
            SortedDictionary<string, ApiRequest> requests)
        {
            _name = name;
            Requests = requests;
        }        
    }
}
