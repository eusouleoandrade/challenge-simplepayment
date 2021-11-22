using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.Interfaces;
using Application.UseCases;

namespace Application.UseCases
{
    public class GetCustomerTransactionsUseCase : ValuableBaseUseCase, IGetCustomerTransactionsUseCase
    {
        public GetCustomerTransactionsUseCaseResponseModel Handler(GetCustomerTransactionsUseCaseRequestModel request)
        {
            throw new System.NotImplementedException();
        }

        public override bool Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}