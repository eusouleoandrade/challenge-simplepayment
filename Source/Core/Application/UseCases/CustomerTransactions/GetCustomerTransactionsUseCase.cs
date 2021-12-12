using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.Interfaces;
using AutoMapper;
using Binbin.Linq;
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

            var transactions = await _transactionRepository.GetByFilters(GenerateFilters(requestModel));
            var responseModel = _mapper.Map<List<GetCustomerTransactionsUseCaseResponseModel>>(transactions);

            return responseModel.OrderBy(o => o.CreationDate).ToList();
        }

        private void Validate(GetCustomerTransactionsUseCaseRequestModel requestModel)
        {
            if (requestModel.CustomerId == Guid.Empty)
                AddErrorNotification(new NotificationMessage("CustomerId is required"));

            if (requestModel.Product == null & requestModel.CreditCardBrand == null & requestModel.Status == null & requestModel.CreationDate == null)
                AddErrorNotification(new NotificationMessage("Two filters are required"));
        }

        private Expression<Func<Transaction, bool>> GenerateFilters(GetCustomerTransactionsUseCaseRequestModel requestModel)
        {
            Expression<Func<Transaction, bool>> finalExpression = t => t.CustomerId == requestModel.CustomerId;

            if (requestModel.Status.HasValue)
            {
                Expression<Func<Transaction, bool>> statusExpression = t => t.Status == requestModel.Status;
                finalExpression = PredicateBuilder.And(finalExpression, statusExpression);
            }

            return finalExpression;
        }
    }
}