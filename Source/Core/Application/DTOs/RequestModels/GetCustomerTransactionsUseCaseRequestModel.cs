using System;
using Domain.Enums;

namespace Application.DTOs.RequestModel
{
    public class GetCustomerTransactionsUseCaseRequestModel
    {
        public Guid CustomerId { get; set; }

        public Product Product { get; set; }

        public CreditCardBrand CreditCardBrand { get; set; }

        public StatusTransaction Status { get; set; }

        public DateTime CreationDate { get; set; }
    }
}