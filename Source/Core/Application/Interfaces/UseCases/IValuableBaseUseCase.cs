using System.Collections.Generic;

namespace Application.Interfaces.UseCases
{
    public interface IValuableBaseUseCase
    {
        bool Valid { get; }

        bool Invalid { get; }

        List<string> ValidationResult { get; }
    }
}