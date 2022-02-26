using System;
using Application.DTOs.RequestModels;
using Application.DTOs.ResponseModels;
using Application.Interfaces;
using Application.Mappings;
using Application.UseCases;
using AutoMapper;
using Domain.Enums;
using Moq;
using Xunit;

namespace Application.UnitTest.UseCases
{
    public class CreateTransactionUseCaseHandlerTest
    {
        private Moq.Mock<ITransactionRepositoryAsync> _transactionRepositoryMock;
        private Moq.Mock<IGetCustomerUseCase> _getCustomerUseCaseMock;
        private IMapper _mapperMock;
        private ICreateTransactionUseCase _createTransactionUseCase;

        public CreateTransactionUseCaseHandlerTest()
        {
            _transactionRepositoryMock = new Moq.Mock<ITransactionRepositoryAsync>();
            _getCustomerUseCaseMock = new Moq.Mock<IGetCustomerUseCase>();

            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();
        }

        /// <summary>
        /// Should execute sucessfully
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "CreateTransactionUseCase - Must run successfully")]
        public async void ShouldExecuteSucessfully()
        {
            // Arranje
            var customerId = Guid.NewGuid();

            var requestModel = new CreateTransactionUseCaseRequestModel(
                12.88m,
                Product.BankSlip,
                CreditCardBrand.Braspag,
                1,
                customerId
            );

            var customerResponseModel = new GetCustomerUseCaseResponseModel
            {
                Id = customerId,
                Name = "Leandro"
            };

            _getCustomerUseCaseMock.Setup(x => x.Handler(It.IsAny<Guid>())).ReturnsAsync(customerResponseModel);

            _createTransactionUseCase = new CreateTransactionUseCase(_transactionRepositoryMock.Object, _mapperMock, _getCustomerUseCaseMock.Object);

            // Act
            var responseModel = await _createTransactionUseCase.Handler(requestModel);

            // Assert
            Assert.Empty(_createTransactionUseCase.ErrorNotificationResult);
            Assert.False(_createTransactionUseCase.HasErrorNotification);
            Assert.Equal(requestModel.Value, responseModel.Value);
            Assert.Equal(requestModel.Product, responseModel.Product);
            Assert.Equal(requestModel.CreditCardBrand, responseModel.CreditCardBrand);
            Assert.Equal(requestModel.NumberOfInstallments, responseModel.NumberOfInstallments);
            Assert.Equal(StatusTransaction.Confirmed, responseModel.Status);
            Assert.True(responseModel.CreationDate.Date == DateTime.Now.Date);
        }

        /// <summary>
        /// Check handler validation failure when customerId not valid
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "CreateTransactionUseCase - Should not run when CustomerId is not valid")]
        public async void ShoudNotExecute_WhenCustomerIdNotValid()
        {
            // Arranje
            var customerId = Guid.Empty;

            var requestModel = new CreateTransactionUseCaseRequestModel(
                12.88m,
                Product.BankSlip,
                CreditCardBrand.Braspag,
                1,
                customerId
            );

            _createTransactionUseCase = new CreateTransactionUseCase(_transactionRepositoryMock.Object, _mapperMock, _getCustomerUseCaseMock.Object);

            // Act
            var responseModel = await _createTransactionUseCase.Handler(requestModel);

            // Assert
            Assert.NotEmpty(_createTransactionUseCase.ErrorNotificationResult);
            Assert.True(_createTransactionUseCase.HasErrorNotification);
            Assert.Contains(_createTransactionUseCase.ErrorNotificationResult, item => item.Message == "Customer not found for the submitted CustomerId");
            Assert.Contains(_createTransactionUseCase.ErrorNotificationResult, item => item.Message == "CustomerId field is not valid");
            Assert.Null(responseModel);
        }

        /// <summary>
        /// Check handler validation Failure when value is zero or negative
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Theory(DisplayName = "CreateTransactionUseCase - Should not run when value is zero or negative")]
        [InlineData(0)]
        [InlineData(-20.99)]
        public async void ShouldNotExecute_WhenValueIsZeroOrNegative(decimal value)
        {
            // Arranje
            var requestModel = new CreateTransactionUseCaseRequestModel(
                value,
                Product.BankSlip,
                CreditCardBrand.Braspag,
                1,
                Guid.NewGuid()
            );

            _createTransactionUseCase = new CreateTransactionUseCase(_transactionRepositoryMock.Object, _mapperMock, _getCustomerUseCaseMock.Object);

            // Act
            var responseModel = await _createTransactionUseCase.Handler(requestModel);

            // Assert
            Assert.NotEmpty(_createTransactionUseCase.ErrorNotificationResult);
            Assert.True(_createTransactionUseCase.HasErrorNotification);
            Assert.Contains(_createTransactionUseCase.ErrorNotificationResult, item => item.Message == "Value field must be greater than 0");
            Assert.Null(responseModel);
        }

        /// <summary>
        /// Check handler validation failure when product not Valid
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "CreateTransactionUseCase - Should not run when Product is not valid")]
        public async void ShouldNotExecute_WhenProductNotValid()
        {
            // Arranje
            var requestModel = new CreateTransactionUseCaseRequestModel(
                12.88m,
                Product.Credit,
                CreditCardBrand.Braspag,
                1,
                Guid.NewGuid()
            );

            _createTransactionUseCase = new CreateTransactionUseCase(_transactionRepositoryMock.Object, _mapperMock, _getCustomerUseCaseMock.Object);

            // Act
            var responseModel = await _createTransactionUseCase.Handler(requestModel);

            // Assert
            Assert.NotEmpty(_createTransactionUseCase.ErrorNotificationResult);
            Assert.True(_createTransactionUseCase.HasErrorNotification);
            Assert.Contains(_createTransactionUseCase.ErrorNotificationResult, item => item.Message == "If the credit card brand is Braspag, the product must be a bank slip");
            Assert.Null(responseModel);
        }

        /// <summary>
        /// Check handler validation failure when credit card brand not valid
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "CreateTransactionUseCase - Should not run when CreditCardBrand not valid")]
        public async void ShouldNotExecute_WhenCreditCardBrandNotValid()
        {
            // Arranje
            var requestModel = new CreateTransactionUseCaseRequestModel(
                12.88m,
                Product.BankSlip,
                CreditCardBrand.Master,
                1,
                Guid.NewGuid()
            );

            _createTransactionUseCase = new CreateTransactionUseCase(_transactionRepositoryMock.Object, _mapperMock, _getCustomerUseCaseMock.Object);

            // Act
            var responseModel = await _createTransactionUseCase.Handler(requestModel);

            // Assert
            Assert.NotEmpty(_createTransactionUseCase.ErrorNotificationResult);
            Assert.True(_createTransactionUseCase.HasErrorNotification);
            Assert.Contains(_createTransactionUseCase.ErrorNotificationResult, item => item.Message == "If the product is bank slip, the credit card brand must be a Braspag");
            Assert.Null(responseModel);
        }

        /// <summary>
        /// Check handler validation failure when number of installments not valid
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "CreateTransactionUseCase - Should not run when NumberOfInstallments not valid")]
        public async void ShouldNotExecute_WhenNumberOfInstallmentsNotValid()
        {
            // Arranje
            var requestModel = new CreateTransactionUseCaseRequestModel(
                12.88m,
                Product.Debit,
                CreditCardBrand.Master,
                2,
                Guid.NewGuid()
            );

            _createTransactionUseCase = new CreateTransactionUseCase(_transactionRepositoryMock.Object, _mapperMock, _getCustomerUseCaseMock.Object);

            // Act
            var responseModel = await _createTransactionUseCase.Handler(requestModel);

            // Assert
            Assert.NotEmpty(_createTransactionUseCase.ErrorNotificationResult);
            Assert.True(_createTransactionUseCase.HasErrorNotification);
            Assert.Contains(_createTransactionUseCase.ErrorNotificationResult,
                item => item.Message == "If Product is debit or bank slip the number of installments field must be equal to 1");
            Assert.Null(responseModel);
        }

        /// <summary>
        /// Check handler validation failure when number of installments is zero or negative
        /// </summary>
        /// <returns></returns>
        [Theory(DisplayName = "CreateTransactionUseCase - Should not run when NumberOfInstallments is zero or negativa")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async void ShouldNotExecute_WhenNumberOfInstallmentsIsZeroOrNegative(int numberOfInstallments)
        {
            // Arranje
            var requestModel = new CreateTransactionUseCaseRequestModel(
                12.88m,
                Product.Credit,
                CreditCardBrand.Master,
                numberOfInstallments,
                Guid.NewGuid()
            );

            _createTransactionUseCase = new CreateTransactionUseCase(_transactionRepositoryMock.Object, _mapperMock, _getCustomerUseCaseMock.Object);

            // Act
            var responseModel = await _createTransactionUseCase.Handler(requestModel);

            // Assert
            Assert.NotEmpty(_createTransactionUseCase.ErrorNotificationResult);
            Assert.True(_createTransactionUseCase.HasErrorNotification);
            Assert.Contains(_createTransactionUseCase.ErrorNotificationResult, item => item.Message == "Number of installments field must be greater than 0");
            Assert.Null(responseModel);
        }
    }
}