using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entites;

public class CreditCardPayment : Payment
{
    public CreditCardPayment(
        DateTime paidDate,
        DateTime expireDate,
        Address address,
        decimal total,
        decimal totalPaid,
        Document document,
        string payer,
        string cardHolderName,
        string cardNumber,
        string lastTransactionNumber) : base(paidDate, expireDate, address, total, totalPaid, document, payer)
    {
        CardHolderName = cardHolderName;
        CardNumber = cardNumber;
        LastTransactionNumber = lastTransactionNumber;
    }

    public string CardHolderName { get; private set; }

    public string CardNumber { get; private set; }

    public string LastTransactionNumber { get; private set; }
}