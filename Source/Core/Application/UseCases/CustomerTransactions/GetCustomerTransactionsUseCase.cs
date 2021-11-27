using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.Interfaces;
using AutoMapper;
using Infra.Notification.Abstrations;
using Infra.Notification.Models;

namespace Application.UseCases
{
    public class GetCustomerTransactionsUseCase : Notifiable, IGetCustomerTransactionsUseCase
    {
        private readonly ITransactionRepositoryAsync _transactionRepository;
        private readonly IMapper _mapper;

        public GetCustomerTransactionsUseCase(ITransactionRepositoryAsync transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetCustomerTransactionsUseCaseResponseModel>> Handler(GetCustomerTransactionsUseCaseRequestModel requestModel)
        {
            Validate(requestModel);

            if (HasErrorNotification)
                return default;

            var transactions = await _transactionRepository.GetAllAsync();
            var responseModel =  _mapper.Map<List<GetCustomerTransactionsUseCaseResponseModel>>(transactions);
            
            return responseModel;
        }

        private void Validate(GetCustomerTransactionsUseCaseRequestModel requestModel)
        {
            if (requestModel.CustomerId == Guid.Empty)
                AddErrorNotification(new NotificationMessage("CustomerId is required"));

            if (requestModel.Product == null & requestModel.CreditCardBrand == null & requestModel.Status == null & requestModel.CreationDate == null)
                AddErrorNotification(new NotificationMessage("Two filters are required"));
        }
    }
}