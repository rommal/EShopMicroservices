using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models.ValueObjects
{
    public record OrderId
    {
        private OrderId(Guid value) => Value = value;

        public Guid Value { get; }

        public static OrderId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
                throw new DomainException("OrderId cannot be empty.");

            return new OrderId(value);
        }
    }
}
