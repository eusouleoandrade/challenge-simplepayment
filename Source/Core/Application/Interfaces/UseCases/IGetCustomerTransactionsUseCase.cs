using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Infra.Notification.Interfaces;

namespace Application.Interfaces
{
    public interface IGetCustomerTransactionsUseCase : INotifiable, IUseCase<GetCustomerTransactionsUseCaseRequestModel, GetCustomerTransactionsUseCaseResponseModel>
    {
    }
}