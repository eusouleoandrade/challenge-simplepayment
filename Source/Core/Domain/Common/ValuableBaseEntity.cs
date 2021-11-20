using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Common
{
    public abstract class ValuableBaseEntity<TId> where TId : struct
    {
        public TId Id { get; set; }

        [NotMapped]
        public bool Valid => !ValidationResult.Any();

        [NotMapped]
        public bool Invalid => !Valid;

        [NotMapped]
        public IList<string> ValidationResult { get; protected set; }

        public ValuableBaseEntity()
        {
            ValidationResult = new List<string>();
        }

        public abstract bool Validate();
    }
}