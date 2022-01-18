using Application.DTOs.RequestModels;
using Infra.Notification.Interfaces;

namespace Application.Interfaces
{
    public interface ICreateTransactionUseCase : INotifiable, IUseCase<CreateTransactionRequestModel>
    {
    }
}