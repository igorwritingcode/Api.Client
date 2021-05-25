using System;
using Api.Common;
using Api.Common.Auth;
using Api.Client.IfCarApiSe.Dtos;

public class QuoteRequestBody
{
    [JsonPropertyName("PriceCalculationParameters")]
    public PriceCalculationParameters PriceCalculationParameters { get; set; }
    [JsonPropertyName("PaymentOptions")]
    public PaymentOptions PaymentOptions { get; set; }
    [JsonPropertyName("Vehicle")]
    public Vehicle Vehicle { get; set; }
    [JsonPropertyName("InsurancePolicyHolder")]
    public InsurancePolicyHolder InsurancePolicyHolder { get; set; }
    [JsonPropertyName("PartnerIdentity")]
    public PartnerIdentity PartnerIdentity { get; set; }
}
public class PriceCalculationParameters
{
    [JsonPropertyName("maximumMileagePerYearInKilometers")]
    public string maximumMileagePerYearInKilometers { get; set; }
    [JsonPropertyName("wantedMaximumCarDamageDeductible")]
    public string wantedMaximumCarDamageDeductible { get; set; }
    [JsonPropertyName("isDealerPrice")]
    public boolean isDealerPrice { get; set; }
    [JsonPropertyName("hasHadAccident")]
    public boolean hasHadAccident { get; set; }
}
public class PaymentOptions
{
    [JsonPropertyName("paymentFrequencyAndMethodCode")]
    public string paymentFrequencyAndMethodCode { get; set; }
}
public class Vehicle
{
    [JsonPropertyName("vehicleIdentity")]
    [JsonPropertyName("modelIdentity")]
    [JsonPropertyName("brand")]
    public string brand { get; set; }
}
public class InsurancePolicyHolder
{
    [JsonPropertyName("socialSecurityNumberSv")]
    public string socialSecurityNumberSv { get; set; }
    [JsonPropertyName("companyIdentityNumberSv")]
    public string companyIdentityNumberSv { get; set; }
}
public class PartnerIdentity
{
    [JsonPropertyName("portfolio")]
    public string portfolio { get; set; }
    [JsonPropertyName("authorizedDealerId")]
    public string authorizedDealerId { get; set; }
    [JsonPropertyName("bundledProduct")]
}

