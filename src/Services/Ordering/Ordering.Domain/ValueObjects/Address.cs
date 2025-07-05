namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string? EmailAddress { get; init; } = default!;
        public string AddressLine { get; init; } = default!;
        public string State { get; init; } = default!;
        public string ZipCode { get; init; } = default!;
        public string Country { get; init; } = default!;

        protected Address() { }

        private Address(string firstName, string lastName, string? emailAddress, string addressLine, string state, string zipCode, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            AddressLine = addressLine;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public static Address of(string firstName, string lastName, string? emailAddress, string addressLine, string state, string zipCode, string country)
        {
            ArgumentNullException.ThrowIfNull(firstName, nameof(firstName));
            ArgumentNullException.ThrowIfNull(lastName, nameof(lastName));
            ArgumentNullException.ThrowIfNull(addressLine, nameof(addressLine));
            ArgumentNullException.ThrowIfNull(state, nameof(state));
            ArgumentNullException.ThrowIfNull(zipCode, nameof(zipCode));
            ArgumentNullException.ThrowIfNull(country, nameof(country));
            return new Address(firstName, lastName, emailAddress, addressLine, state, zipCode, country);
        }
    }
}
