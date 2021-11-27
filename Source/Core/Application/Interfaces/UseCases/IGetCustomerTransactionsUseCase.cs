using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Infra.Shared.Interfaces;

namespace Application.Interfaces
{
    public interface IGetCustomerTransactionsUseCase : INotifiable, IUseCase<GetCustomerTransactionsUseCaseRequestModel, GetCustomerTransactionsUseCaseResponseModel>
    {
    }
}