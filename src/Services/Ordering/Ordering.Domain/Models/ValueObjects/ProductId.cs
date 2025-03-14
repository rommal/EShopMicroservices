using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models.ValueObjects
{
    public record ProductId
    {
        private ProductId(Guid value) => Value = value;

        public Guid Value { get; }

        public static ProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
                throw new DomainException("ProductId cannot be empty.");

            return new ProductId(value);
        }
    }
}
