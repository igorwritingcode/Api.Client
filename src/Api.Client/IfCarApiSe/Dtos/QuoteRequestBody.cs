namespace Api.Client.IfCarApiSe.Dtos
{
    public class QuoteRequestBody
    {
        public Vehicle Vehicle { get; set; }
        public InsurancePolicyHolder InsurancePolicyHolder { get; set; }
        public PartnerIdentity PartnerIdentity { get; set; }
    }

    public class PartnerIdentity
    {
        public string Portfolio { get; set; }
        public string AuthorizedDealerId { get; set; }
        public BundledProduct BundledProduct { get; set; }
    }

    public class BundledProduct
    {
        public bool IsBundledProduct { get; set; }
        public string BundledProductId { get; set; }
    }

    public class InsurancePolicyHolder
    {
        public string SocialSecurityNumberSv { get; set; }
        public string CompanyIdentityNumberSv { get; set; }
    }

    public class Vehicle
    {
        public VehicleIdentity VehicleIdentity { get; set; }
        public string Brand { get; set; }
    }

    public class VehicleIdentity
    {
        public string RegistrationNumberSv { get; set; }
        public string VehicleIdentificationNumber { get; set; }
    }
}
