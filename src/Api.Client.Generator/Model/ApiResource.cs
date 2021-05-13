using System.Collections.Generic;

namespace Api.Client.Generator.Model
{
    //Represents resource in client API model
    public class ApiResource
    {
        private readonly string _name;
        private SortedDictionary<string, ApiRequest> _requests;

        public ApiResource(
            string name,
            SortedDictionary<string, ApiRequest> requests)
        {
            _name = name;
            _requests = requests;
        }

        public SortedDictionary<string, ApiRequest> GetRequests() => _requests;
        
    }
}
