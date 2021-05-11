using Api.Common.Auth;
using Api.Common;

namespace Api.Client.IfCarApiSe
{
    public class IfCarApiSeClient : BaseClient
    {
        public override string Name => "IfCarApiSe";
        public QuoteInsuranceResource Insurance { get; private set; }
        public IfCarApiSeClient(ICredential credentials, string baseUrl) : base(credentials, baseUrl)
        {
            Insurance = new QuoteInsuranceResource(this);
        }
    }
}
