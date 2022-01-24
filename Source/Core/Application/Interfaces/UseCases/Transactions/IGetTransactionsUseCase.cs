using System.Collections.Generic;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Infra.Notification;

namespace Application.Interfaces
{
    public interface IGetTransactionsUseCase : INotifiable, IUseCase<GetTransactionsUseCaseRequestModel, List<GetTransactionsUseCaseResponseModel>>
    {
    }
}