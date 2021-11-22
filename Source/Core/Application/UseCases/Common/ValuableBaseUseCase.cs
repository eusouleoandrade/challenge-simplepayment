using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.UseCases;

namespace Application.UseCases
{
    public abstract class ValuableBaseUseCase : IValuableBaseUseCase
    {
        protected List<string> _validationResult;

        public bool Valid => !ValidationResult.Any();

        public bool Invalid => !Valid;

        public List<string> ValidationResult => _validationResult;

        public ValuableBaseUseCase()
        {
            _validationResult = new List<string>();
        }

        protected void AddValidationMessage(string message)
        {
            if (!string.IsNullOrEmpty(message) || !string.IsNullOrWhiteSpace(message))
                _validationResult.Add(message);
        }
    }
}