using Flunt.Validations;
using PaymentContext.Shared.Entites;

namespace PaymentContext.Domain.Entites;

public class Subscription : Entity
{
    private readonly IList<Payment> _payments;

    public Subscription(DateTime? expireDate)
    {
        Active = true;
        CreateDate = DateTime.UtcNow;
        LastUpdateDate = DateTime.UtcNow;
        ExpireDate = expireDate;
        _payments = new List<Payment>();
    }

    public bool Active { get; private set; }

    public DateTime CreateDate { get; private set; }

    public DateTime LastUpdateDate { get; private set; }

    public DateTime? ExpireDate { get; private set; }

    public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }

    public void AddPayment(Payment payment) 
    {
        AddNotifications(new Contract()
            .Requires()
            .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento deve ser futura")
        );

        _payments.Add(payment);
    }

    public void Activate() {
        Active = true;
        LastUpdateDate = DateTime.UtcNow;
    }

    public void Inactivate() {
        Active = false;
        LastUpdateDate = DateTime.UtcNow;
    }
}