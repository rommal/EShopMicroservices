using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Domain.Models.ValueObjects
{
    [ComplexType]
    public record Address
    {
        protected Address()
        {
            
        }

        private Address(string firstName, string lastName, string emailAdress, string addressLine, string country, string state, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAdress;
            AddressLine = addressLine;
            Country = country;
            State = state;
            ZipCode = zipCode;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string? EmailAddress { get; }
        public string AddressLine { get; }
        public string Country { get; }
        public string State { get; }
        public string ZipCode { get; }

        public static Address Of(string firstName, string lastName, string emailAdress, string addressLine, string country, string state, string zipCode)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrWhiteSpace(lastName, nameof(lastName));

            return new Address(firstName, lastName, emailAdress, addressLine, country, state, zipCode);
        }
    }
}
