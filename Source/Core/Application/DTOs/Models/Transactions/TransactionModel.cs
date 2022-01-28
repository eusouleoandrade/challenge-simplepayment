using System;
using Domain.Enums;
using Newtonsoft.Json;

namespace Application.DTOs.Models
{
    public class TransactionModel
    {
        [JsonProperty("transaction_id")]
        public Guid TransacionId { get; set; }

        public decimal Value { get; set; }

        public Product Product { get; set; }

        [JsonProperty("credit_card_brand")]
        public CreditCardBrand CreditCardBrand { get; set; }

        [JsonProperty("number_installments")]
        public int NumberOfInstallments { get; set; }

        public StatusTransaction Status { get; set; }

        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }
    }
}