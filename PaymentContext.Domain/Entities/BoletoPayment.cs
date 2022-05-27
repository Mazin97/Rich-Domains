using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entites;

public class BoletoPayment : Payment
{
    public BoletoPayment(
        DateTime paidDate,
        DateTime expireDate,
        Address address,
        decimal total,
        decimal totalPaid,
        Document document,
        string payer,
        string barCode,
        Email email,
        string boletoNumber) : base(paidDate, expireDate, address, total, totalPaid, document, payer)
    {
        BarCode = barCode;
        Email = email;
        BoletoNumber = boletoNumber;
    }

    public string BarCode { get; private set; }

    public Email Email { get; private set; }

    public string BoletoNumber { get; private set; }
}