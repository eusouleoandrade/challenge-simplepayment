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
        [Fact]
        public async void CheckSuccessHandler()
        {
            // Arranje
            var customerId = Guid.NewGuid();

            IReadOnlyList<Transaction> transactions = new List<Transaction>(){
                new Transaction(){Id = Guid.NewGuid(),
                                        Value = 12.99m,
                                        Product = Product.Credit,
                                        CreditCardBrand = CreditCardBrand.Master,
                                        NumberOfInstallments = 1,
                                        CustomerId = customerId,
                                        Status = StatusTransaction.Confirmed,
                                        CreationDate = DateTime.Now },

                new Transaction(){Id = Guid.NewGuid(),
                                        Value = 35.98m,
                                        Product = Product.Credit,
                                        CreditCardBrand = CreditCardBrand.Master,
                                        NumberOfInstallments = 1,
                                        CustomerId = customerId,
                                        Status = StatusTransaction.Confirmed,
                                        CreationDate = DateTime.Now },
            };

            Moq.Mock<ITransactionRepositoryAsync> repositoryMock = new Mock<ITransactionRepositoryAsync>();
            repositoryMock.Setup(x => x.GetByFilters(It.IsAny<Expression<Func<Transaction, bool>>>())).ReturnsAsync(transactions);

            var mapperMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            var mapper = mapperMock.CreateMapper();

            var getCustomerTransactionsUseCase = new GetCustomerTransactionsUseCase(repositoryMock.Object, mapper);

            var requestModel = new GetCustomerTransactionsUseCaseRequestModel{
                CustomerId = customerId,
                Product = Product.BankSlip
            };

            // Act
            var result = await getCustomerTransactionsUseCase.Handler(requestModel);

            // Assert
            Assert.True(result.Count > 0);
        }
    }
}