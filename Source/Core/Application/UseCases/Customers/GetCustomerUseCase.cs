using System;
using System.Threading.Tasks;
using Application.DTOs.ResponseModels;
using Application.Interfaces;
using AutoMapper;
using Infra.Notification.Abstrations;

namespace Application.UseCases
{
    public class GetCustomerUseCase : Notifiable, IGetCustomerUseCase
    {
        private readonly ICustomerRepositoryAsync _customerRespositoryAsync;
        private readonly IMapper _mapper;

        public GetCustomerUseCase(ICustomerRepositoryAsync customerRespositoryAsync, IMapper mapper)
        {
            _customerRespositoryAsync = customerRespositoryAsync;
            _mapper = mapper;
        }

        public async Task<GetCustomerUseCaseResponseModel> Handler(Guid id)
        {
            Validate(id);

            if (HasErrorNotification)
                return await Task.FromResult<GetCustomerUseCaseResponseModel>(default);

            var customer = await _customerRespositoryAsync.GetByIdAsync(id);

            return _mapper.Map<GetCustomerUseCaseResponseModel>(customer);
        }

        private void Validate(Guid id)
        {
            if (id == Guid.Empty)
                AddErrorNotification("Id is required");
        }
    }
}