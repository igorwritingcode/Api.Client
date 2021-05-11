using Api.Client.IfCarApiSe.Dtos.Response;
using Api.Common;

namespace Api.Client.IfCarApiSe.Dtos
{
    public class QuoteRequest : ClientBaseRequest<QuoteResponse>
    {
        QuoteRequestBody Body { get; init; }
        public override string MethodName => "quote";
        public override string RestPath => "request-quote-branded-car-insurance-se/v1/Insurance/quote";
        public override string HttpMethod => "POST";
        public QuoteRequest(IClient client, QuoteRequestBody body) : base(client)
        {
            Body = body;
        }

        protected override object GetBody() => Body;
    }
}
