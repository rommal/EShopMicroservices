﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Models.ValueObjects
{
    [ComplexType]
    public record Payment
    {
        private const int CVVLength = 3;

        protected Payment()
        {

        }

        private Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public string? CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName, nameof(cardName));
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber, nameof(cardNumber));
            ArgumentException.ThrowIfNullOrWhiteSpace(expiration, nameof(expiration));
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv, nameof(cvv));
            ArgumentOutOfRangeException.ThrowIfNotEqual(cvv, nameof(cvv));

            return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
        }
    }
}
