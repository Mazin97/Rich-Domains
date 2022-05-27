using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entites;

namespace PaymentContext.Domain.Entites;

public abstract class Payment : Entity
{
    protected Payment(DateTime paidDate, DateTime expireDate, Address address, decimal total, decimal totalPaid, Document document, string payer)
    {
        Number = new Guid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
        PaidDate = paidDate;
        ExpireDate = expireDate;
        Address = address;
        Total = total;
        TotalPaid = totalPaid;
        Document = document;
        Payer = payer;

        AddNotifications(new Contract()
            .Requires()
            .IsGreaterThan(Total, 0, "Payment.Total", "O total deve ser maior que zero")
            .IsGreaterOrEqualsThan(Total, TotalPaid, "Payment.TotalPaid", "O valor pago é menor que o valor do boleto")
        );
    }

    public string Number { get; private set; }

    public DateTime PaidDate { get; private set; }

    public DateTime ExpireDate { get; private  set; }

    public Address Address { get; private set; }

    public decimal Total { get; private set; }

    public decimal TotalPaid { get; private set; }

    public Document Document { get; private set; }

    public string Payer { get; private set; }
}