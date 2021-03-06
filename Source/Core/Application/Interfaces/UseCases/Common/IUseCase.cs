using System.Threading.Tasks;

namespace Application.Interfaces
{
    // public interface IUseCase<TRequest, TResponse>
    //     where TRequest : class where TResponse : class
    // {
    //     Task<TResponse> Handler(TRequest request);
    // }

    public interface IUseCase<TRequest, TResponse>
    {
        Task<TResponse> Handler(TRequest request);
    }

    public interface IUseCase<TRequest>
        where TRequest : class
    {
        Task Handler(TRequest request);
    }
}