using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public class DocumentTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenCNPJIsInvalid()
    {
        var doc = new Document("123", EDocumentType.CNPJ);
        Assert.IsTrue(doc.Invalid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenCNPJIsValid()
    {
        var doc = new Document("34247661000181", EDocumentType.CNPJ);
        Assert.IsTrue(doc.Valid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenCPFIsInvalid()
    {
        var doc = new Document("123", EDocumentType.CPF);
        Assert.IsTrue(doc.Invalid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("08923245232")]
    [DataRow("08923245252")]
    [DataRow("08963245232")]
    public void ShouldReturnErrorWhenCPFIsValid(string cpf)
    {
        var doc = new Document(cpf, EDocumentType.CPF);
        Assert.IsTrue(doc.Valid);
    }
}