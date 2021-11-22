using System.Threading.Tasks;
using Application.Interfaces.UseCases;

namespace Application.Interfaces
{
    public interface IUseCase<TRequest, TResponse> : IValuableBaseUseCase
        where TRequest : class where TResponse : class
    {
        Task<TResponse> Handler(TRequest request);
    }

    public interface IUseCase<TRequest> : IValuableBaseUseCase
        where TRequest : class
    {
        void Handler(TRequest request);
    }
}