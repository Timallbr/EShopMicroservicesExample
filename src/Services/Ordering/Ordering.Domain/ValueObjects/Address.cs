namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string? EmailAddress { get; } = default!;
        public string AddressLine { get; } = default!;
        public string City { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;
        public string Country { get; } = default!;

        protected Address()
        {

        }

        private Address(string firstName, string lastName, string emailAddress, string addressLine, string city, string state, string zipCode, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string city, string state, string zipCode, string country)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

            return new Address(firstName, lastName, emailAddress, addressLine, city, state, zipCode, country);
        }
    }
}
