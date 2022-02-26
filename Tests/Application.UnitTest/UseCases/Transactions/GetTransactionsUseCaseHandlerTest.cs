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
    public class GetTransactionsUseCaseHandlerTest
    {
        private readonly Moq.Mock<ITransactionRepositoryAsync> _transactionRepositoryMock;
        private readonly IMapper _mapperMock;
        private IGetTransactionsUseCase _getTransactionsUseCase;

        public GetTransactionsUseCaseHandlerTest()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepositoryAsync>();

            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();
        }

        /// <summary>
        /// Verify success in nandler
        /// </summary>
        [Fact(DisplayName = "GetTransactionUseCase - Must run successfully")]
        public async void ShouldExecuteSucessfully()
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

            _getTransactionsUseCase = new GetTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            var requestModel = new GetTransactionsUseCaseRequestModel
            {
                CustomerId = customerId,
                Product = Product.Credit
            };

            // Act
            var responseModel = await _getTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.NotEmpty(responseModel);
            Assert.NotNull(responseModel);
            Assert.False(_getTransactionsUseCase.HasErrorNotification);
            Assert.Empty(_getTransactionsUseCase.ErrorNotificationResult);
        }

        /// <summary>
        /// Check handler failure when customerId is empty
        /// </summary>
        [Fact(DisplayName = "GetTransactionUseCase - Should not run when CustomerId is empty")]
        public async void ShouldNotExecute_WhenCustomerIdIsEmpty()
        {
            // Arranje
            _getTransactionsUseCase = new GetTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            var requestModel = new GetTransactionsUseCaseRequestModel
            {
                CustomerId = Guid.Empty,
                Product = Product.Credit
            };

            // Act
            var responseModel = await _getTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.True(_getTransactionsUseCase.HasErrorNotification);
            Assert.False(_getTransactionsUseCase.HasSuccessNotification);
            Assert.NotEmpty(_getTransactionsUseCase.ErrorNotificationResult);
            Assert.Empty(_getTransactionsUseCase.SuccessNotificationResult);
            Assert.Null(responseModel);
            Assert.Contains(_getTransactionsUseCase.ErrorNotificationResult, item => item.Message == "CustomerId is required");
        }

        /// <summary>
        /// Check handler failure when only Id is sent
        /// </summary>
        [Fact(DisplayName = "GetTransactionUseCase - Should not run when only Id is sent")]
        public async void ShouldNotExecute_WhenOnlyIdIsSent()
        {
            // Arranje
            _getTransactionsUseCase = new GetTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            var requestModel = new GetTransactionsUseCaseRequestModel
            {
                CustomerId = Guid.NewGuid()
            };

            // Act
            var responseModel = await _getTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.True(_getTransactionsUseCase.HasErrorNotification);
            Assert.False(_getTransactionsUseCase.HasSuccessNotification);
            Assert.NotEmpty(_getTransactionsUseCase.ErrorNotificationResult);
            Assert.Empty(_getTransactionsUseCase.SuccessNotificationResult);
            Assert.Null(responseModel);
            Assert.Contains(_getTransactionsUseCase.ErrorNotificationResult, item => item.Message == "Two filters are required");
        }
    }
}