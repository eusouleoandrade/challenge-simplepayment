using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;

namespace Application.Interfaces
{
    public interface IGetCustomerTransactionsUseCase : IUseCase<GetCustomerTransactionsUseCaseRequestModel, GetCustomerTransactionsUseCaseResponseModel>
    {
    }
}