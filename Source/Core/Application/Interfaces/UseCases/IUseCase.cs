namespace Application.Interfaces
{
    public interface IUseCase<TRequest, TResponse>
        where TRequest : class where TResponse : class
    {
        TResponse Handler(TRequest request);
    }

    public interface IUseCase<TRequest>
    where TRequest : class
    {
        void Handler(TRequest request);
    }
}