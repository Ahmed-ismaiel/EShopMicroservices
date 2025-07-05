namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {

        public string? CardName { get; init; } = default!;
        public string CardNumber { get; init; } = default!;
        public string ExpirationDate { get; init; } = default!;
        public string Cvv { get; init; } = default!;
        public string PaymentMethod { get; init; } = default!;

        protected Payment() { }
        private Payment(string? cardName, string cardNumber, string expirationDate, string cvv, string paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            Cvv = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment of(string? cardName, string cardNumber, string expirationDate, string cvv, string paymentMethod)
        {
            ArgumentNullException.ThrowIfNull(cardNumber, nameof(cardNumber));
            ArgumentNullException.ThrowIfNull(expirationDate, nameof(expirationDate));
            ArgumentNullException.ThrowIfNull(cvv, nameof(cvv));
            ArgumentNullException.ThrowIfNull(paymentMethod, nameof(paymentMethod));
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 13 || cardNumber.Length > 19)
            {
                throw new ArgumentException("Card number must be between 13 and 19 characters long.", nameof(cardNumber));
            }
            if (string.IsNullOrWhiteSpace(expirationDate) || !DateTime.TryParse(expirationDate, out _))
            {
                throw new ArgumentException("Expiration date is not valid.", nameof(expirationDate));
            }
            if (string.IsNullOrWhiteSpace(cvv) || cvv.Length < 3 || cvv.Length > 4)
            {
                throw new ArgumentException("CVV must be 3 or 4 characters long.", nameof(cvv));
            }
            return new Payment(cardName, cardNumber, expirationDate, cvv, paymentMethod);
        }


    }
}
