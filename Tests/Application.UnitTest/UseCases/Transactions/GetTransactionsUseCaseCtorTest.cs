using Application.Interfaces;
using Application.Mappings;
using Application.UseCases;
using AutoMapper;
using Moq;
using Xunit;

namespace Application.UnitTest.UseCases
{
    public class GetTransactionsUseCaseCtorTest
    {
        private Moq.Mock<ITransactionRepositoryAsync> _transactionRepositoryMock;
        private IMapper _mapperMock;
        private IGetTransactionsUseCase _getTransactionsUseCase;

        [Fact]
        public void VerifySuccessCtor()
        {
            // Arranje
            _transactionRepositoryMock = new Mock<ITransactionRepositoryAsync>();

            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();

            // Act
            _getTransactionsUseCase = new GetTransactionsUseCase(_transactionRepositoryMock.Object, _mapperMock);

            // Assert
            Assert.NotNull(_getTransactionsUseCase);
            Assert.Empty(_getTransactionsUseCase.ErrorNotificationResult);
            Assert.Empty(_getTransactionsUseCase.SuccessNotificationResult);
            Assert.False(_getTransactionsUseCase.HasErrorNotification);
            Assert.False(_getTransactionsUseCase.HasSuccessNotification);
        }
    }
}