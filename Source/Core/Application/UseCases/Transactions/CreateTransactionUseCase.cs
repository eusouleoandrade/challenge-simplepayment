using System;
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

        private readonly IMapper _mapper;

        public CreateTransactionUseCase(ITransactionRepositoryAsync transactionRepository, IMapper mapper)
        {
            _transactionRespository = transactionRepository;
            _mapper = mapper;
        }

        public async Task Handler(CreateTransactionRequestModel request)
        {
            // TODO: Check if customer existed

            var transaction = _mapper.Map<Transaction>(request);
            
            transaction.CreationDate = DateTime.Now;
            transaction.Status = StatusTransaction.Confirmed;
            
            await _transactionRespository.AddAsync(transaction);
        }
    }
}