using System;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Transaction : ValuableBaseEntity<Guid>
    {
        public decimal Value { get; set; }
        public Product Product { get; set; }
        public CreditCardBrand CreditCardBrand { get; set; }
        public int NumberOfInstallments { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer {get; set;}

        public Transaction()
        {
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}