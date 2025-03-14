﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models.ValueObjects
{
    public record OrderId
    {
        public Guid Value { get; }
    }
}
