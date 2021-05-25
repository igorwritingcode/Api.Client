using System;
using Api.Common;
using Api.Common.Auth;
using Api.Client.IfCarApiSe.Dtos;

public class UpdateRequestBody
{
    [JsonPropertyName("PriceCalculationParameters")]
    public PriceCalculationParameters PriceCalculationParameters { get; set; }
    [JsonPropertyName("PaymentOptions")]
    public PaymentOptions PaymentOptions { get; set; }
    [JsonPropertyName("Id")]
    public string Id { get; set; }
    [JsonPropertyName("SelectedInsuranceCoverageId")]
    public string SelectedInsuranceCoverageId { get; set; }
    [JsonPropertyName("SelectedAddOnCoverageIds")]
    public string[] SelectedAddOnCoverageIds { get; set; }
    [JsonPropertyName("BundledProductId")]
    public string BundledProductId { get; set; }
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
    public VehicleIdentity vehicleIdentity { get; set; }
    [JsonPropertyName("modelIdentity")]
    public ModelIdentity modelIdentity { get; set; }
    [JsonPropertyName("brand")]
    public string brand { get; set; }
}
public class VehicleIdentity
{
    [JsonPropertyName("registrationNumberSv")]
    public string registrationNumberSv { get; set; }
    [JsonPropertyName("vehicleIdentificationNumber")]
    public string vehicleIdentificationNumber { get; set; }
}
public class ModelIdentity
{
    [JsonPropertyName("variantId")]
    public string variantId { get; set; }
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
    public BundledProduct bundledProduct { get; set; }
}
public class BundledProduct
{
    [JsonPropertyName("isBundledProduct")]
    public boolean isBundledProduct { get; set; }
    [JsonPropertyName("bundledProductId")]
    public string bundledProductId { get; set; }
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

