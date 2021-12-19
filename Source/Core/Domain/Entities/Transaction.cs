using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Transaction : BaseEntity<Guid>
    {
        public decimal Value { get; set; }
        
        public Product Product { get; set; }
        
        public CreditCardBrand CreditCardBrand { get; set; }
        
        public StatusTransaction Status { get; set; }
        
        public int NumberOfInstallments { get; set; }

        public DateTime CreationDate { get; set; }
        
        public Guid CustomerId { get; set; }
        
        public virtual Customer Customer {get; set;}

        public Transaction()
        {
        }

        public Transaction(decimal value
            , Product product
            , CreditCardBrand creditCardBrand
            , int numberOfInstallments
            , Guid customerId
            , StatusTransaction status
            , DateTime creationDate)
        {
            Value = value;
            Product = product;
            CreditCardBrand = creditCardBrand;
            NumberOfInstallments = numberOfInstallments;
            CustomerId = customerId;
            Status = status;
            CreationDate = creationDate;
        }
    }
}