using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers;

public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>, IHandler<CreatePayPalSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        command.Validate();
        if (command.Invalid) 
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar sua assinatura.");
        }

        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");

        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está em uso");

        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.Zipcode);

        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.UtcNow.AddMonths(1));
        var payment = new BoletoPayment(command.PaidDate, command.ExpireDate, address, command.Total, command.TotalPaid, new Document(command.PayerDocument, command.PayerDocumentType), command.Payer, command.BarCode, email, command.BoletoNumber);

        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        AddNotifications(name, document, email, address, subscription, payment);

        if (Invalid)
            return new CommandResult(false, "Não foi possível realizar sua assinatura");

        _repository.CreateSubscription(student);

        _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem-vindo", "Sua assinatura foi criada");

        return new CommandResult(true, "Assinatura realizada com sucesso");
    }

    public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
    {
        command.Validate();
        if (command.Invalid) 
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar sua assinatura.");
        }

        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");

        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está em uso");

        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.Zipcode);

        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.UtcNow.AddMonths(1));
        var payment = new PayPalPayment(
            command.PaidDate,
            command.ExpireDate,
            address, command.Total,
            command.TotalPaid,
            new Document(command.PayerDocument, command.PayerDocumentType),
            command.Payer,
            email,
            command.TransactionCode
        );

        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        AddNotifications(name, document, email, address, subscription, payment);

        if (Invalid)
            return new CommandResult(false, "Não foi possível realizar sua assinatura");

        _repository.CreateSubscription(student);

        _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem-vindo", "Sua assinatura foi criada");

        return new CommandResult(true, "Assinatura realizada com sucesso");
    }
}