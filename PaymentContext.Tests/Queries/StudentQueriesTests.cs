using PaymentContext.Domain.Entites;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public class StudentQueriesTests
{
    private IList<Student> _students;

    public StudentQueriesTests()
    {
        _students = new List<Student>();

        for (var i = 0; i <= 10; i ++) 
        {
            _students.Add(
                new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("12324231212", Domain.Enums.EDocumentType.CPF),
                    new Email($"aluno{i}@teste.com")
                )
            );
        }
    }

    [TestMethod]
    public void ShouldreturnNullWhenDocumentNotExists()
    {
        var exp = StudentQueries.GetStudentInfo("12345678911");

        var student = _students.AsQueryable().Where(exp).FirstOrDefault();

        Assert.AreEqual(null, student);
    }

    [TestMethod]
    public void ShouldreturnStudentWhenDocumentExists()
    {
        var exp = StudentQueries.GetStudentInfo("12324231212");

        var student = _students.AsQueryable().Where(exp).FirstOrDefault();

        Assert.AreNotEqual(null, student);
    }
}