using Api.Client.IfCarApiSe.Dtos;
using Api.Common;

namespace Api.Client.IfCarApiSe
{
    public class QuoteInsuranceResource
    {
        IClient Client { get; init; }

        public QuoteInsuranceResource(IClient client)
        {
            Client = client;
        }

        public QuoteRequest CreateQuote(QuoteRequestBody body)
        {
            return new QuoteRequest(Client, body);
        }
    }
}
