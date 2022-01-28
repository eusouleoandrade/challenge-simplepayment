using Application.DTOs.RequestModels;
using Application.DTOs.ResponseModels;
using Infra.Notification;

namespace Application.Interfaces
{
    public interface ICreateTransactionUseCase : INotifiable, IUseCase<CreateTransactionUseCaseRequestModel, CreateTransactionUseCaseResponseModel>
    {
    }
}