using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.Interfaces;

namespace Application.Interfaces
{
    public interface IGetCustomerTransactionsUseCase : IUseCase<GetCustomerTransactionsUseCaseRequestModel, GetCustomerTransactionsUseCaseResponseModel>
    {
    }
}