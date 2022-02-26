using Application.Interfaces;
using Application.Mappings;
using Application.UseCases;
using AutoMapper;
using Xunit;

namespace Application.UnitTest.UseCases
{
    public class GetCustomerUseCaseCtorTest
    {
        private Moq.Mock<ICustomerRepositoryAsync> _customerRepositoryMock;
        private IMapper _mapperMock;
        private IGetCustomerUseCase _getCustomerUseCase;

        /// <summary>
        /// Verify success in ctor
        /// </summary>
        [Fact(DisplayName = "GetCustomerUseCase - Must run successfully")]
        public void VerifySuccessCtor()
        {
            // Arranje
            _customerRepositoryMock = new Moq.Mock<ICustomerRepositoryAsync>();
            
            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();

            // Act
            _getCustomerUseCase = new GetCustomerUseCase(_customerRepositoryMock.Object, _mapperMock);

            // Assert
            Assert.NotNull(_getCustomerUseCase);
            Assert.Empty(_getCustomerUseCase.ErrorNotificationResult);
            Assert.Empty(_getCustomerUseCase.SuccessNotificationResult);
            Assert.False(_getCustomerUseCase.HasErrorNotification);
            Assert.False(_getCustomerUseCase.HasSuccessNotification);
        }
    }
}