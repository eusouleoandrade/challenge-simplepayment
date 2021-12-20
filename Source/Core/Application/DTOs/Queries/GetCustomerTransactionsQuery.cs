using System;
using System.Collections.Generic;
using Application.DTOs.Models;
using Newtonsoft.Json;

namespace Application.DTOs.Queries
{
    public class GetCustomerTransactionsQuery
    {
        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }

        public List<TransactionModel> Transactions { get; set; }
    }
}