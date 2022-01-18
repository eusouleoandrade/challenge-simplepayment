using System;
using Application.DTOs.ResponseModels;
using Infra.Notification.Interfaces;

namespace Application.Interfaces
{
    public interface IGetCustomerUseCase : INotifiable, IUseCase<Guid, GetCustomerUseCaseResponseModel>
    {
    }
}