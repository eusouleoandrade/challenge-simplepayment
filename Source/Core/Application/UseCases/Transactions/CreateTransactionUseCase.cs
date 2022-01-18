using System;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs.RequestModels;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Infra.Notification.Abstrations;

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

        public async Task Handler(CreateTransactionRequestModel request)
        {
            Validade(request);

            if (HasErrorNotification)
                return;

            var transaction = _mapper.Map<Transaction>(request);

            transaction.CreationDate = DateTime.Now;
            transaction.Status = StatusTransaction.Confirmed;

            await _transactionRespository.AddAsync(transaction);
        }

        private void Validade(CreateTransactionRequestModel request)
        {
            var customerResponseModel = _getCustomerUseCase.Handler(request.CustomerId).Result;

            if (customerResponseModel == null)
                AddErrorNotification("Customer not found for the submitted CustomerId");
        }
    }
}