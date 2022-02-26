using System;
using Application.Interfaces;
using Application.Mappings;
using Application.UseCases;
using AutoMapper;
using Domain.Entities;
using Moq;
using Xunit;

namespace Application.UnitTest.UseCases
{
    public class GetCustomerUseCaseHandlerTest
    {
        private readonly Moq.Mock<ICustomerRepositoryAsync> _customerRepositoryMock;
        private readonly IMapper _mapperMock;
        private IGetCustomerUseCase _getCustomerUseCase;

        public GetCustomerUseCaseHandlerTest()
        {
            _customerRepositoryMock = new Mock<ICustomerRepositoryAsync>();

            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();
        }

        /// <summary>
        /// Verify success in handler
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [Theory(DisplayName = "GetCustomerUseCase - Must run successfully")]
        [InlineData("7a6f8e46-fce7-4bcc-a46b-64e49ba8ebb2", "Leandro")]
        [InlineData("580588c0-4d79-4760-bc2a-85cf10cc5940", "Rafael")]
        [InlineData("36c1f298-3fb4-4127-a794-230a2f9acbf3", "José")]
        [InlineData("e36c3044-ec1e-4b11-ac71-5fbff8c14f38", "Ricardo")]
        [InlineData("a2e13426-de86-4ad5-96e7-3ba0053c6b29", "Marcos")]
        public async void ShouldExecuteSucessfully(Guid customerId, string customerName)
        {
            // Arranje
            var customer = new Customer { Id = customerId, Name = customerName };

            _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(customer);

            _getCustomerUseCase = new GetCustomerUseCase(_customerRepositoryMock.Object, _mapperMock);

            // Act
            var responseModel = await _getCustomerUseCase.Handler(customerId);

            // Assert
            Assert.NotNull(responseModel);
            Assert.Equal(customer.Name, responseModel.Name);
            Assert.Equal(customer.Id, responseModel.Id);
            Assert.False(_getCustomerUseCase.HasErrorNotification);
            Assert.Empty(_getCustomerUseCase.ErrorNotificationResult);
        }

        /// <summary>
        /// Check handler validation failure when id is empty
        /// </summary>
        /// <returns></returns>
        [Fact(DisplayName = "GetCustomerUseCase - Should not run when not sending customerId")]
        public async void ShouldNotExecute_WhenNotSendingCusomerId()
        {
            // Arranje
            var customerId = Guid.Empty;
            _getCustomerUseCase = new GetCustomerUseCase(_customerRepositoryMock.Object, _mapperMock);

            // Act
            var responseModel = await _getCustomerUseCase.Handler(customerId);

            // Assert
            Assert.True(_getCustomerUseCase.HasErrorNotification);
            Assert.False(_getCustomerUseCase.HasSuccessNotification);
            Assert.NotEmpty(_getCustomerUseCase.ErrorNotificationResult);
            Assert.Empty(_getCustomerUseCase.SuccessNotificationResult);
            Assert.Null(responseModel);
            Assert.Contains(_getCustomerUseCase.ErrorNotificationResult, item => item.Message == "Id is required");
        }
    }
}