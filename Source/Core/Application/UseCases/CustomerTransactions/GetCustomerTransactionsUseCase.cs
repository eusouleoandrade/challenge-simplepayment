using System;
using System.Threading.Tasks;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.Interfaces;

namespace Application.UseCases
{
    public class GetCustomerTransactionsUseCase : ValuableBaseUseCase, IGetCustomerTransactionsUseCase
    {
        public async Task<GetCustomerTransactionsUseCaseResponseModel> Handler(GetCustomerTransactionsUseCaseRequestModel request)
        {
            Validate(request);

            await Task.CompletedTask;

            if(Invalid)
                return null;

            return null;
        }

        public void Validate(GetCustomerTransactionsUseCaseRequestModel request)
        {
            if(request.CustomerId == Guid.Empty)
                AddValidationMessage("CustomerId is required");
            
            if(request.Product == null || request.CreditCardBrand == null || request.Status == null || request.CreationDate == null)
                AddValidationMessage("Two filters are required");
        }
    }
}