using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entites;

namespace PaymentContext.Domain.Entites;

public class Student : Entity
{
    private IList<Subscription> _subscriptions;

    public Student(Name name, Document document, Email email)
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();

        AddNotifications(name, document, email);
    }

    public Name Name { get; set; }

    public Document Document { get; private set; }

    public Email Email { get; private set; }

    public Address Address { get; private set; }

    public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

    public void AddSubscription(Subscription subscription)
    {        
        var hasSubscriptionActive = _subscriptions.Any(x => x.Active == true);

        AddNotifications(new Contract()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
            .IsGreaterThan(subscription.Payments.Count(), 0, "Subscript.Payments", "É necessário haver um método de pagamento")
        );

        _subscriptions.Add(subscription);
    }
}