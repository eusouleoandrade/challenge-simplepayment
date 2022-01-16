using System.Collections.Generic;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Infra.Notification.Interfaces;

namespace Application.Interfaces
{
    public interface IGetTransactionsUseCase : INotifiable, IUseCase<GetTransactionsUseCaseRequestModel, List<GetTransactionsUseCaseResponseModel>>
    {
    }
}