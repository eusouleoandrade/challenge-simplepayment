using Application.Interfaces;
using Application.Mappings;
using Application.UseCases;
using AutoMapper;
using Moq;
using Xunit;

namespace Application.UnitTest.UseCases
{
    public class GetCustomerTransactionsUseCaseCtorTest
    {
        private Moq.Mock<ITransactionRepositoryAsync> _transactionRepositoryMock;
        private IMapper _mapperMock;
        private IGetCustomerTransactionsUseCase _getCustomerTransactionsUseCase;

        [Fact]
        public void VerifySuccessCtor()
        {
            // Arranje
            _transactionRepositoryMock = new Mock<ITransactionRepositoryAsync>();

            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();

            // Act
            _getCustomerTransactionsUseCase = new GetCustomerTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            // Assert
            Assert.NotNull(_getCustomerTransactionsUseCase);
            Assert.Empty(_getCustomerTransactionsUseCase.ErrorNotificationResult);
            Assert.Empty(_getCustomerTransactionsUseCase.SuccessNotificationResult);
            Assert.False(_getCustomerTransactionsUseCase.HasErrorNotification);
            Assert.False(_getCustomerTransactionsUseCase.HasSuccessNotification);
        }
    }
}