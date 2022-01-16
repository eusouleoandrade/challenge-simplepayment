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

        public CreateTransactionRequestModel()
        {
            Validate();
        }

        private void Validate()
        {
            if (Value == decimal.Zero
            || NumberOfInstallments == decimal.Zero
            || NumberOfInstallments == decimal.Zero
            || CustomerId == Guid.Empty)
                AddErrorNotification("All fields are required");

            if (CreditCardBrand == Domain.Enums.CreditCardBrand.Braspag)
            {
                if (Product != Domain.Enums.Product.BankSlip)
                    AddErrorNotification("If the credit card brand is Braspag, the product must be a bank slip");
            }
            else
            {
                if (Product == Domain.Enums.Product.BankSlip)
                    AddErrorNotification("If the product is a bank slip, the credit card brand cannot be Braspag");
            }

            // TODO: Continuar daqui
        }
    }
}