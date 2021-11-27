using System;
using Domain.Enums;

namespace Application.DTOs.Filters
{
    public class GetCustomerTransactionsFilter
    {
        public Guid CustomerId { get; set; }
    
        public Product? Product { get; set; }

        public CreditCardBrand? CreditCardBrand { get; set; }

        public StatusTransaction? Status { get; set; }

        public DateTime? CreationDate { get; set; }

        public GetCustomerTransactionsFilter(Guid customerId, Product? product, CreditCardBrand? creditCardBrand, StatusTransaction? status, DateTime? creationDate)
        {
            CustomerId = customerId;
            Product = product;
            CreditCardBrand = creditCardBrand;
            Status = status;
            CreationDate = creationDate;
        }
    }
}