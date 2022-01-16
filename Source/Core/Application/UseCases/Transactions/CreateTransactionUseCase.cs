using Application.DTOs.RequestModels;
using Application.Interfaces;
using Infra.Notification.Abstrations;

namespace Application.UseCases.Transactions
{
    public class CreateTransactionUseCase : Notifiable, ICreateTransactionUseCase
    {
        public void Handler(CreateTransactionRequestModel request)
        {
            throw new System.NotImplementedException();
        }
    }
}