using System;
using System.Threading.Tasks;
using Application.DTOs.RequestModels;
using Application.DTOs.ResponseModels;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infra.Notification;

namespace Application.UseCases
{
    public class CreateTransactionUseCase : Notifiable, ICreateTransactionUseCase
    {
        private readonly ITransactionRepositoryAsync _transactionRespository;
        private readonly IGetCustomerUseCase _getCustomerUseCase;
        private readonly IMapper _mapper;

        public CreateTransactionUseCase(ITransactionRepositoryAsync transactionRepository, IMapper mapper, IGetCustomerUseCase getCustomerUseCase)
        {
            _transactionRespository = transactionRepository;
            _mapper = mapper;
            _getCustomerUseCase = getCustomerUseCase;
        }

        public async Task<CreateTransactionUseCaseResponseModel> Handler(CreateTransactionUseCaseRequestModel request)
        {
            Validade(request);

            if (HasErrorNotification)
                return await Task.FromResult<CreateTransactionUseCaseResponseModel>(default);

            var transaction = _mapper.Map<Transaction>(request);

            transaction.CreationDate = DateTime.Now;
            transaction.Status = StatusTransaction.Confirmed;

            await _transactionRespository.AddAsync(transaction);

            var responseModel = _mapper.Map<CreateTransactionUseCaseResponseModel>(transaction);

            return await Task.FromResult<CreateTransactionUseCaseResponseModel>(responseModel);
        }

        private void Validade(CreateTransactionUseCaseRequestModel request)
        {
            if (request.HasErrorNotification)
                AddErrorNotification(request.ErrorNotificationResult);

            var customerResponseModel = _getCustomerUseCase.Handler(request.CustomerId).Result;

            if (customerResponseModel == null)
                AddErrorNotification("Customer not found for the submitted CustomerId");
        }
    }
}