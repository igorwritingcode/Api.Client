using System;
using System.Collections.Generic;

namespace Api.Client.IfCarApiSe.Dtos.Response
{
    public class QuoteResponse
    {
        public string Id { get; set; }
        public List<InsuranceCoverage> InsuranceCoverages { get; set; }
        public List<CoverageItemDetail> CoverageItemDetails { get; set; }
        public Vehicle Vehicle { get; set; }
        public InsurancePolicyHolder InsurancePolicyHolder { get; set; }
        public PaymentOptions PaymentOptions { get; set; }
        public object BundledProduct { get; set; }
        public List<PrePurchaseInformation> PrePurchaseInformation { get; set; }
        public Mileage Mileage { get; set; }
        public List<Link> Links { get; set; }
    }

    public class CoverageItemDetail
    {
        public Guid Id { get; set; }
        public string NameSv { get; set; }
        public string DescriptionSv { get; set; }
    }

    public class InsuranceCoverage
    {
        public Guid CoverageId { get; set; }
        public string CoverageNameSv { get; set; }
        public string InsuranceBrand { get; set; }
        public bool IsRecommentedByIf { get; set; }
        public List<Guid> CoverageItemsIncluded { get; set; }
        public Price Price { get; set; }
        public List<AddOnProduct> AddOnProducts { get; set; }
        public Deductible Deductible { get; set; }
    }

    public class AddOnProduct
    {
        public Guid Id { get; set; }
        public string NameSv { get; set; }
        public string DescriptionSv { get; set; }
        public bool IsIncluded { get; set; }
        public bool IsSelected { get; set; }
        public Price Price { get; set; }
    }

    public class Price
    {
        public long Amount { get; set; }
        public Currency Currency { get; set; }
        public CostTypeSv CostTypeSv { get; set; }
        public string ProductName { get; set; }
    }

    public class Deductible
    {
        public string DeductibleUsedInCalculation { get; set; }
        public List<string> AvailableDeductibles { get; set; }
    }

    public class InsurancePolicyHolder
    {
        public string CompanyName { get; set; }
        public string CompanyIdentityNumberSv { get; set; }
    }

    public class Link
    {
        public string Rel { get; set; }
        public Uri Href { get; set; }
        public string Method { get; set; }
    }

    public class Mileage
    {
        public long MileageUsedInCalculationInKilometers { get; set; }
        public List<string> AvailableMileagesInKilometers { get; set; }
    }

    public class PaymentOptions
    {
        public string PaymentFrequencyAndMethodCode { get; set; }
        public List<AvailablePaymentFrequencyAndMethodSv> AvailablePaymentFrequencyAndMethodSv { get; set; }
    }

    public class AvailablePaymentFrequencyAndMethodSv
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class PrePurchaseInformation
    {
        public Uri Link { get; set; }
        public string Name { get; set; }
    }

    public class Vehicle
    {
        public string RegistrationNumberSv { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public long CarModelYear { get; set; }
        public DateTimeOffset CarDamageWarrantyExpirationDate { get; set; }
        public string AdditionalVehicleInformation { get; set; }
        public string StatusCar { get; set; }
    }

    public enum CostTypeSv { Halvår };

    public enum Currency { Kr };
}
