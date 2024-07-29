using CrudTestMicroservice.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;

namespace CrudTest.Application.UnitTest.Common.Exceptions;

public class ValidationExceptionTests
{
    [Test]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        var actual = new ValidationException().Errors;

        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Test]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("FirstName", "Required input '{PropertyName}' is missing."),
        };

        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "FirstName" });
        actual["FirstName"].Should().BeEquivalentTo(new string[] { "Required input '{PropertyName}' is missing." });
    }

    [Test]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("DateOfBirth", "Date of Birth cannot be in the future"),
            new ValidationFailure("DateOfBirth", "Invalid Date Of Birth"),
            new ValidationFailure("BankAccountNumber", "BankAccountNumber Must Be 26 Digit"),
            new ValidationFailure("BankAccountNumber", "Invalid IBAN"),
        };

        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo(new string[] { "DateOfBirth", "BankAccountNumber" });

        actual["DateOfBirth"].Should().BeEquivalentTo(new string[]
        {
            "Date of Birth cannot be in the future",
            "Invalid Date Of Birth",
        });

        actual["BankAccountNumber"].Should().BeEquivalentTo(new string[]
        {
            "BankAccountNumber Must Be 26 Digit",
            "Invalid IBAN",
        });
    }
}