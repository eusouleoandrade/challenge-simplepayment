using Application.Interfaces;
using Application.Mappings;
using Application.UseCases;
using AutoMapper;
using Moq;
using Xunit;

namespace Application.UnitTest.UseCases
{
    public class CreateTransactionUseCaseCtorTest
    {
        private Moq.Mock<ITransactionRepositoryAsync> _transactionRepositoryMock;
        private Moq.Mock<IGetCustomerUseCase> _getCustomerUseCaseMock;
        private IMapper _mapperMock;
        private ICreateTransactionUseCase _createTransactionUseCase;

        /// <summary>
        /// Verify success ctor
        /// </summary>
        [Fact(DisplayName = "CreateTransactionUseCase - Must run successfully")]
        public void VerifySuccessCtor()
        {
            // Arranje
            _transactionRepositoryMock = new Mock<ITransactionRepositoryAsync>();
            _getCustomerUseCaseMock = new Mock<IGetCustomerUseCase>();

            var mapperConfigurationMock = new MapperConfiguration(cfg => cfg.AddProfile(new GeneralProfile()));
            _mapperMock = mapperConfigurationMock.CreateMapper();

            // Act
            _createTransactionUseCase = new CreateTransactionUseCase(_transactionRepositoryMock.Object, _mapperMock, _getCustomerUseCaseMock.Object);

            // Assert
            Assert.NotNull(_createTransactionUseCase);
            Assert.Empty(_createTransactionUseCase.ErrorNotificationResult);
            Assert.Empty(_createTransactionUseCase.SuccessNotificationResult);
            Assert.False(_createTransactionUseCase.HasErrorNotification);
            Assert.False(_createTransactionUseCase.HasSuccessNotification);
        }
    }
}