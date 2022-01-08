using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Application.DTOs.RequestModel;
using Application.Interfaces;
using Application.Mappings;
using Application.UseCases;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Moq;
using Xunit;

namespace Application.UnitTest.UseCases
{
    public class GetCustomerTransactionsUseCaseHandlerTest
    {
        private readonly Moq.Mock<ITransactionRepositoryAsync> _transactionRepositoryMock;
        private readonly IMapper _mapperMock;
        private IGetCustomerTransactionsUseCase _getCustomerTransactionsUseCase;

        public GetCustomerTransactionsUseCaseHandlerTest()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepositoryAsync>();

            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();
        }

        /// <summary>
        /// Verify success in nandler
        /// </summary>
        [Fact]
        public async void VerifySuccessInHandler()
        {
            // Arranje
            var customerId = Guid.NewGuid();
            var creationDate = new DateTime(2022, 01, 02);

            IReadOnlyList<Transaction> transactions = new List<Transaction>(){
                new Transaction(){Id = Guid.NewGuid(),
                                        Value = 12.99m,
                                        Product = Product.Credit,
                                        CreditCardBrand = CreditCardBrand.Master,
                                        NumberOfInstallments = 1,
                                        CustomerId = customerId,
                                        Status = StatusTransaction.Confirmed,
                                        CreationDate = creationDate },

                new Transaction(){Id = Guid.NewGuid(),
                                        Value = 35.98m,
                                        Product = Product.Credit,
                                        CreditCardBrand = CreditCardBrand.Master,
                                        NumberOfInstallments = 1,
                                        CustomerId = customerId,
                                        Status = StatusTransaction.Confirmed,
                                        CreationDate = creationDate },
            };

            _transactionRepositoryMock.Setup(x => x.GetByFilters(It.IsAny<Expression<Func<Transaction, bool>>>())).ReturnsAsync(transactions);

            _getCustomerTransactionsUseCase = new GetCustomerTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            var requestModel = new GetCustomerTransactionsUseCaseRequestModel
            {
                CustomerId = customerId,
                Product = Product.Credit
            };

            // Act
            var responseModel = await _getCustomerTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.NotEmpty(responseModel);
            Assert.NotNull(responseModel);
            Assert.False(_getCustomerTransactionsUseCase.HasErrorNotification);
            Assert.Empty(_getCustomerTransactionsUseCase.ErrorNotificationResult);
        }

        /// <summary>
        /// Check handler failure when customerId is empty
        /// </summary>
        [Fact]
        public async void CheckHandlerValidationFailureWhenCustomerIdIsEmpty()
        {
            // Arranje
            _getCustomerTransactionsUseCase = new GetCustomerTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            var requestModel = new GetCustomerTransactionsUseCaseRequestModel
            {
                CustomerId = Guid.Empty,
                Product = Product.Credit
            };

            // Act
            var responseModel = await _getCustomerTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.True(_getCustomerTransactionsUseCase.HasErrorNotification);
            Assert.False(_getCustomerTransactionsUseCase.HasSuccessNotification);
            Assert.NotEmpty(_getCustomerTransactionsUseCase.ErrorNotificationResult);
            Assert.Empty(_getCustomerTransactionsUseCase.SuccessNotificationResult);
            Assert.Null(responseModel);
            Assert.Contains(_getCustomerTransactionsUseCase.ErrorNotificationResult, item => item.Message == "CustomerId is required");
        }

        /// <summary>
        /// Check handler failure when only Id is sent
        /// </summary>
        [Fact]
        public async void CheckHandlerValidationFailureWhenOnlyIdIsSent()
        {
            // Arranje
            _getCustomerTransactionsUseCase = new GetCustomerTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            var requestModel = new GetCustomerTransactionsUseCaseRequestModel
            {
                CustomerId = Guid.NewGuid()
            };

            // Act
            var responseModel = await _getCustomerTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.True(_getCustomerTransactionsUseCase.HasErrorNotification);
            Assert.False(_getCustomerTransactionsUseCase.HasSuccessNotification);
            Assert.NotEmpty(_getCustomerTransactionsUseCase.ErrorNotificationResult);
            Assert.Empty(_getCustomerTransactionsUseCase.SuccessNotificationResult);
            Assert.Null(responseModel);
            Assert.Contains(_getCustomerTransactionsUseCase.ErrorNotificationResult, item => item.Message == "Two filters are required");
        }
    }
}