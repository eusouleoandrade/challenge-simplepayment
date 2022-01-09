using System;
using Domain.Enums;
using Newtonsoft.Json;

namespace Application.DTOs.Requests
{
    public class CreateTransactionRequest
    {
        [JsonProperty("value")]
        public decimal Value { get; set; }
        
        [JsonProperty("product")]
        public Product Product { get; set; }
        
        [JsonProperty("credit_card_brand")]
        public CreditCardBrand CreditCardBrand { get; set; }
        
        [JsonProperty("number_of_intallments")]
        public int NumberOfInstallments { get; set; }
        
        [JsonProperty("customer_id")]
        public Guid CustomerId { get; set; }
    }
}