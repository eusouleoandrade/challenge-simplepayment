using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Verify success in nandler
        /// </summary>
        [Fact]
        public async void VerifySuccessInHandler()
        {
            // Arranje
            var customerId = Guid.NewGuid();
            var creationDate = new DateTime(2022, 01, 02);
            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            var mapperMock = mapperConfigurationMock.CreateMapper();

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

            Moq.Mock<ITransactionRepositoryAsync> transactionRepositoryMock = new Mock<ITransactionRepositoryAsync>();
            transactionRepositoryMock.Setup(x => x.GetByFilters(It.IsAny<Expression<Func<Transaction, bool>>>())).ReturnsAsync(transactions);

            var getCustomerTransactionsUseCase = new GetCustomerTransactionsUseCase(transactionRepositoryMock.Object, mapperMock);

            var requestModel = new GetCustomerTransactionsUseCaseRequestModel
            {
                CustomerId = customerId,
                Product = Product.Credit
            };

            // Act
            var responseModel = await getCustomerTransactionsUseCase.Handler(requestModel);

            // Assert
            decimal sumOfExpectedTransactions = 48.97m;
            decimal sumOfActualTransaction = responseModel.Sum(s => s.Transactions.Sum(t =>  t.Value));
            DateTime expectedCreationDate = new DateTime(2022, 01, 02);
            DateTime actualCreationDate = responseModel.First().CreationDate;
            int quantityItemsOfResponseModel = responseModel.Count();
            bool hasErrorNotification = getCustomerTransactionsUseCase.HasErrorNotification;
            int quantityItemsErrorNotificationResult = getCustomerTransactionsUseCase.ErrorNotificationResult.Count();

            Assert.Equal(sumOfExpectedTransactions, sumOfActualTransaction);
            Assert.True(quantityItemsOfResponseModel > decimal.Zero);
            Assert.Equal(expectedCreationDate, actualCreationDate);
            Assert.False(hasErrorNotification);
            Assert.Equal(quantityItemsErrorNotificationResult, decimal.Zero);
        }

        /// <summary>
        /// Check handler failure when customerId is null or empty
        /// </summary>
        [Fact]
        public void CheckHandlerFailureWhenCustomerIdIsEmpty()
        {
        }

        /// <summary>
        /// Check handler failure when only Id is sent
        /// </summary>
        [Fact]
        public void CheckHandlerFailureWhenOnlyIdIsSent()
        {
        }
    }
}