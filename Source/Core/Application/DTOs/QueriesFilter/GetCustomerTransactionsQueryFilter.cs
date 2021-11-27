using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Filters
{
    public class GetCustomerTransactionsQueryFilter
    {
        public Product? Product { get; set; }

        public CreditCardBrand? CreditCardBrand { get; set; }

        public StatusTransaction? Status { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}