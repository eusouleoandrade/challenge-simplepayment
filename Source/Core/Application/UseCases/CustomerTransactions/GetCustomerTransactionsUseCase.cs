using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
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

            Expression<Func<Transaction, bool>> filters = x => 
            x.CustomerId == requestModel.CustomerId &&
            x.Product == requestModel.Product &&
            x.CreditCardBrand == requestModel.CreditCardBrand &&
            x.Status == requestModel.Status &&
            x.CreationDate == requestModel.CreationDate;

            var transactions = await _transactionRepository.GetByFilters(filters);
            var responseModel =  _mapper.Map<List<GetCustomerTransactionsUseCaseResponseModel>>(transactions);

            return responseModel.OrderBy(o => o.CreationDate).ToList();
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