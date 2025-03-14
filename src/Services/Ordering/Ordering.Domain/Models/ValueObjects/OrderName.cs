﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 1;
        private OrderName(string value) => Value = value;

        public string Value { get; }

        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);
            
            return new OrderName(value);
        }
    }
}
