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

            var getCustomerTransactionsUseCase = new GetCustomerTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            var requestModel = new GetCustomerTransactionsUseCaseRequestModel
            {
                CustomerId = customerId,
                Product = Product.Credit
            };

            // Act
            var responseModel = await getCustomerTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.NotEmpty(responseModel);
            Assert.NotNull(responseModel);
            Assert.False(getCustomerTransactionsUseCase.HasErrorNotification);
            Assert.Empty(getCustomerTransactionsUseCase.ErrorNotificationResult);
        }

        /// <summary>
        /// Check handler failure when customerId is empty
        /// </summary>
        [Fact]
        public async void CheckHandlerValidationFailureWhenCustomerIdIsEmpty()
        {
            // Arranje
            var getCustomerTransactionsUseCase = new GetCustomerTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            var requestModel = new GetCustomerTransactionsUseCaseRequestModel
            {
                CustomerId = Guid.Empty,
                Product = Product.Credit
            };

            // Act
            var responseModel = await getCustomerTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.True(getCustomerTransactionsUseCase.HasErrorNotification);
            Assert.False(getCustomerTransactionsUseCase.HasSuccessNotification);
            Assert.NotEmpty(getCustomerTransactionsUseCase.ErrorNotificationResult);
            Assert.Empty(getCustomerTransactionsUseCase.SuccessNotificationResult);
            Assert.Null(responseModel);
            Assert.Contains(getCustomerTransactionsUseCase.ErrorNotificationResult, item => item.Message == "CustomerId is required");
        }

        /// <summary>
        /// Check handler failure when only Id is sent
        /// </summary>
        [Fact]
        public async void CheckHandlerValidationFailureWhenOnlyIdIsSent()
        {
            // Arranje
            var getCustomerTransactionsUseCase = new GetCustomerTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            var requestModel = new GetCustomerTransactionsUseCaseRequestModel
            {
                CustomerId = Guid.NewGuid()
            };

            // Act
            var responseModel = await getCustomerTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.True(getCustomerTransactionsUseCase.HasErrorNotification);
            Assert.False(getCustomerTransactionsUseCase.HasSuccessNotification);
            Assert.NotEmpty(getCustomerTransactionsUseCase.ErrorNotificationResult);
            Assert.Empty(getCustomerTransactionsUseCase.SuccessNotificationResult);
            Assert.Null(responseModel);
            Assert.Contains(getCustomerTransactionsUseCase.ErrorNotificationResult, item => item.Message == "Two filters are required");
        }
    }
}