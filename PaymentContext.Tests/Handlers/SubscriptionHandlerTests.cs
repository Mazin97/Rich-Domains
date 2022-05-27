using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests;

[TestClass]
public class SubscriptionHandlerTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists() 
    {
        var handler = new SubscriptionHandler(
            new FakeStudentRepository(),
            new FakeEmailService()
        );

        var command = new CreateBoletoSubscriptionCommand();

        command.BarCode = "1234567890";
        command.BoletoNumber = "123";
        command.City = "Curitiba";
        command.Country = "Brazil";
        command.Document = "99999999999";
        command.Email = "hello@hotmail.io";
        command.ExpireDate = DateTime.UtcNow;
        command.FirstName = "Bruce";
        command.LastName = "Wayne";
        command.Neighborhood = "Cajuru";
        command.Number = "123";
        command.PaidDate = DateTime.UtcNow;
        command.Payer = "José";
        command.PayerDocument = "12345678911";
        command.PayerDocumentType = Domain.Enums.EDocumentType.CPF;
        command.PayerEmail = "teste@teste.com";
        command.PaymentNumber = "123123";
        command.State = "Paraná";
        command.Street = "Rua dos Bobos";
        command.Total = 60;
        command.TotalPaid = 60;
        command.Zipcode = "82932492";

        handler.Handle(command);
        Assert.AreEqual(false, handler.Valid);
    }
}