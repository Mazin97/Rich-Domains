using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entites;

public class PayPalPayment : Payment
{
    public PayPalPayment(
        DateTime paidDate,
        DateTime expireDate,
        Address address,
        decimal total,
        decimal totalPaid,
        Document document,
        string payer,
        Email email,
        string transactionCode) : base(paidDate, expireDate, address, total, totalPaid, document, payer)
    {
        Email = email;
        TransactionCode = transactionCode;
    }

    public Email Email { get; private set; }

    public string TransactionCode { get; private set; }
}