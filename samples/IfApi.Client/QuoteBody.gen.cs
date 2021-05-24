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

