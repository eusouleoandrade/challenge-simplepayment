using System.Collections.Generic;
using System.Linq;

namespace Application.UseCases
{
    public abstract class ValuableBaseUseCase
    {
        protected List<string> _validationResult;

        public bool Valid => !ValidationResult.Any();

        public bool Invalid => !Valid;

        public List<string> ValidationResult => _validationResult;

        public ValuableBaseUseCase()
        {
            _validationResult = new List<string>();
        }

        public abstract bool Validate();
    }
}