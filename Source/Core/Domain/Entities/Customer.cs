using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Customer : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public virtual List<Transaction> Transactions { get; set; }

        public Customer(string name)
        {
            Name = name;
        }

        public Customer()
        {
        }
    }
}