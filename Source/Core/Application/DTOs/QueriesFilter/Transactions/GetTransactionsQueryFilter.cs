using System;
using Domain.Enums;

namespace Application.DTOs.Filters
{
    public class GetTransactionsQueryFilter
    {
        public Product? Product { get; set; }

        public CreditCardBrand? CreditCardBrand { get; set; }

        public StatusTransaction? Status { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}