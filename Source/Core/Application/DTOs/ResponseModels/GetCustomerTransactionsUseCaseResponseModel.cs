using System;
using Domain.Enums;

namespace Application.DTOs.ReponseModel
{
    public class GetCustomerTransactionsUseCaseResponseModel
    {
        public Guid TransacionId { get; set; }

        public decimal Value { get; set; }

        public Product Product { get; set; }
        
        public CreditCardBrand CreditCardBrand { get; set; }

        public int NumberOfInstallments { get; set; }

        public StatusTransaction Status { get; set; }
        
        public DateTime CreationDate { get; set; }
    }
}