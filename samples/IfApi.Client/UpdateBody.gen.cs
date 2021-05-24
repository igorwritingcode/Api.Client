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

