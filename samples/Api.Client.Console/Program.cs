using System;
using System.Threading;
using System.Threading.Tasks;
using Api.Client.IfCarApiSe.Dtos;
using Api.Client.IfCarApiSe;
using Api.Common.Auth;

namespace Api.Client.Console
{
    class Program
    {
        static string apiUrl = "https://api.if-insurance.com/test/";
        private static async Task<ClientCredentials> CreateClientCredentials()
        {
            string[] scopes = { Scopes.Quote, Scopes.Buy };

            ClientSecrets secrets = new()
            {
                ClientId = Environment.GetEnvironmentVariable("CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET"),
            };

            var authUrl = Environment.GetEnvironmentVariable("OPENIF_TOKEN_URL");
            return await AuthorizationBroker.AuthorizeAsync(secrets, scopes, authUrl, CancellationToken.None);
        }
        private static QuoteRequestBody CreateBody()
        {
            return new QuoteRequestBody()
            {
                InsurancePolicyHolder = new InsurancePolicyHolder()
                {
                    CompanyIdentityNumberSv = ""
                },
                PartnerIdentity = new PartnerIdentity()
                {
                    AuthorizedDealerId = "VWEB",
                    Portfolio = Portfolio.VRF
                },
                Vehicle = new Vehicle()
                {
                    Brand = "PolestarForetag",
                    VehicleIdentity = new VehicleIdentity()
                    {
                        RegistrationNumberSv = ""
                    }
                }
            };
        }
        static async Task Main(string[] args)
        {
            var creds = await CreateClientCredentials();
            var quoteRequestBody = CreateBody();

            var IfCarApiSeClient = new IfCarApiSeClient(creds, apiUrl);
            var request = IfCarApiSeClient.Insurance.CreateQuote(quoteRequestBody);

            try
            {
                var quote = await request.ExecuteAsync();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}
