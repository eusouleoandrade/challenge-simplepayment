using System;
using System.Collections.Generic;
using Application.DTOs.Models;

namespace Application.DTOs.ReponseModel
{
    public class GetTransactionsUseCaseResponseModel
    {
        public DateTime CreationDate { get; set; }

        public List<TransactionModel> Transactions { get; set; }
    }
}