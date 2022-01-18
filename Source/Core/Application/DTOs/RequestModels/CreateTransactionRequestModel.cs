using System;
using Domain.Enums;
using Infra.Notification.Abstrations;

namespace Application.DTOs.RequestModels
{
    public class CreateTransactionRequestModel : Notifiable
    {
        public decimal Value { get; set; }

        public Product Product { get; set; }

        public CreditCardBrand CreditCardBrand { get; set; }

        public int NumberOfInstallments { get; set; }

        public Guid CustomerId { get; set; }

        public CreateTransactionRequestModel(decimal value, Product product, CreditCardBrand creditCardBrand, int numberOfInstallments, Guid customerId)
        {
            Value = value;
            Product = product;
            CreditCardBrand = creditCardBrand;
            NumberOfInstallments = numberOfInstallments;
            CustomerId = customerId;

            Validate();
        }

        private void Validate()
        {
            if (Value <= decimal.Zero)
                AddErrorNotification("Value field must be greater than 0");

            if (CustomerId == Guid.Empty)
                AddErrorNotification("CustomerId field is not valid");

            if (CreditCardBrand == Domain.Enums.CreditCardBrand.Braspag && Product != Domain.Enums.Product.BankSlip)
                AddErrorNotification("If the credit card brand is Braspag, the product must be a bank slip");

            if (Product == Domain.Enums.Product.BankSlip && CreditCardBrand != Domain.Enums.CreditCardBrand.Braspag)
                AddErrorNotification("If the product is bank slip, the credit card brand must be a Braspag");

            if (NumberOfInstallments == decimal.Zero)
                AddErrorNotification("Number of installments field must be greater than 0");

            if (Product == Domain.Enums.Product.Debit || Product == Domain.Enums.Product.BankSlip)
            {
                if (NumberOfInstallments != 1)
                    AddErrorNotification("If Product is debit or bank slip the number of installments field must be equal to 1");
            }
        }
    }
}