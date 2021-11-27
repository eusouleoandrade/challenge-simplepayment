using System;
using System.Threading.Tasks;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.Interfaces;
using Infra.Notification.Abstrations;
using Infra.Notification.Models;

namespace Application.UseCases
{
    public class GetCustomerTransactionsUseCase : Notifiable, IGetCustomerTransactionsUseCase
    {
        public GetCustomerTransactionsUseCase()
        {    
        }

        public async Task<GetCustomerTransactionsUseCaseResponseModel> Handler(GetCustomerTransactionsUseCaseRequestModel requestModel)
        {
            Validate(requestModel);

            if(HasErrorNotification)
                return default;

            await Task.CompletedTask;

            return null;
        }

        private void Validate(GetCustomerTransactionsUseCaseRequestModel requestModel)
        {
            if (requestModel.CustomerId == Guid.Empty)
                AddErrorNotification(new NotificationMessage("CustomerId is required"));

            if (requestModel.Product == null
            & requestModel.CreditCardBrand == null 
            & requestModel.Status == null 
            & requestModel.CreationDate == null)
                AddErrorNotification(new NotificationMessage("Two filters are required"));
        }
    }
}