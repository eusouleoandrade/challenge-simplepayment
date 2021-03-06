using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.DTOs.Models;
using Application.DTOs.ReponseModel;
using Application.DTOs.RequestModel;
using Application.Interfaces;
using AutoMapper;
using Binbin.Linq;
using Domain.Entities;
using Infra.Notification;

namespace Application.UseCases
{
    public class GetTransactionsUseCase : Notifiable, IGetTransactionsUseCase
    {
        private readonly ITransactionRepositoryAsync _transactionRepository;
        private readonly IMapper _mapper;

        public GetTransactionsUseCase(ITransactionRepositoryAsync transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        public async Task<List<GetTransactionsUseCaseResponseModel>> Handler(GetTransactionsUseCaseRequestModel requestModel)
        {
            Validate(requestModel);

            if (HasErrorNotification)
                return default;

            var filters = GenerateFilters(requestModel);
            var transactions = await _transactionRepository.GetByFilters(filters);

            var responseModel = transactions.GroupBy(g => g.CreationDate.Date)
                .Select(t => new GetTransactionsUseCaseResponseModel
                {
                    CreationDate = t.Key,
                    Transactions = _mapper.Map<List<TransactionModel>>(t.ToList())
                })
                .OrderBy(o => o.CreationDate)
                .ToList();

            return responseModel;
        }

        private void Validate(GetTransactionsUseCaseRequestModel requestModel)
        {
            if (requestModel.CustomerId == Guid.Empty)
                AddErrorNotification(new NotificationMessage("CustomerId is required"));

            if (!requestModel.Product.HasValue && !requestModel.CreditCardBrand.HasValue && !requestModel.Status.HasValue && !requestModel.CreationDate.HasValue)
                AddErrorNotification(new NotificationMessage("Two filters are required"));
        }

        private Expression<Func<Transaction, bool>> GenerateFilters(GetTransactionsUseCaseRequestModel requestModel)
        {
            Expression<Func<Transaction, bool>> finalExpression = t => t.CustomerId == requestModel.CustomerId;

            if (requestModel.Product.HasValue)
            {
                Expression<Func<Transaction, bool>> productExpression = p => p.Product == requestModel.Product;
                finalExpression = PredicateBuilder.And(finalExpression, productExpression);
            }

            if (requestModel.CreditCardBrand.HasValue)
            {
                Expression<Func<Transaction, bool>> creditCardBrandExpression = c => c.CreditCardBrand == requestModel.CreditCardBrand;
                finalExpression = PredicateBuilder.And(finalExpression, creditCardBrandExpression);
            }

            if (requestModel.Status.HasValue)
            {
                Expression<Func<Transaction, bool>> statusExpression = t => t.Status == requestModel.Status;
                finalExpression = PredicateBuilder.And(finalExpression, statusExpression);
            }

            if (requestModel.CreationDate.HasValue)
            {
                Expression<Func<Transaction, bool>> creationDateExpression = c => c.CreationDate.Date == requestModel.CreationDate.Value.Date;
                finalExpression = PredicateBuilder.And(finalExpression, creationDateExpression);
            }

            return finalExpression;
        }
    }
}