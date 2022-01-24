using System;
using Application.DTOs.ResponseModels;
using Infra.Notification;

namespace Application.Interfaces
{
    public interface IGetCustomerUseCase : INotifiable, IUseCase<Guid, GetCustomerUseCaseResponseModel>
    {
    }
}