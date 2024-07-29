using System.Numerics;
using System.Text.RegularExpressions;
using CrudTestMicroservice.Domain.Common.Exceptions;
using CrudTestMicroservice.Domain.Common.Guards;
using PhoneNumbers;

namespace CrudTestMicroservice.Domain.Validations;

public class CustomerValidator
{
    private readonly PhoneNumberUtil _phoneNumberUtil = PhoneNumberUtil.GetInstance();

    public void Validate(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
    {
        if (dateOfBirth > DateTime.Now)
            throw new DomainException("Date of Birth cannot be in the future");

        if (dateOfBirth == DateTime.MinValue || dateOfBirth == DateTime.MaxValue)
            throw new DomainException("Invalid Date Of Birth");

        Guard.Against.NullOrEmpty(firstName, nameof(firstName));

        Guard.Against.NullOrEmpty(lastName, nameof(lastName));

        if (!BeAValidMobileNumber(phoneNumber))
            throw new DomainException("Invalid mobile number");

        if (!IsValidEmail(email))
            throw new DomainException("Invalid email address");

        if (!BeAValidBankAccountNumber(bankAccountNumber))
            throw new DomainException("Invalid IBAN");
    }

    public bool BeAValidMobileNumber(string phoneNumber)
    {
        try
        {
            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrWhiteSpace(phoneNumber) || !phoneNumber.StartsWith("0"))
            {
                return false;
            }

            var parsedNumber = _phoneNumberUtil.Parse(phoneNumber, "IR");
            return _phoneNumberUtil.IsValidNumber(parsedNumber) &&
                   _phoneNumberUtil.GetNumberType(parsedNumber) == PhoneNumberType.MOBILE;
        }
        catch (NumberParseException)
        {
            return false;
        }
    }

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examine the domain part of the email and normalize it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new System.Globalization.IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public bool BeAValidBankAccountNumber(string bankAccountNumber)
    {
        if (string.IsNullOrWhiteSpace(bankAccountNumber) || string.IsNullOrEmpty(bankAccountNumber) || bankAccountNumber.Length != 26 || !bankAccountNumber.StartsWith("IR"))
        {
            return false;
        }

        string modifiedAccountNumber = bankAccountNumber.Substring(4) + bankAccountNumber.Substring(0, 4);
        string convertedNumber = string.Concat(modifiedAccountNumber.Select(c => char.IsLetter(c) ? (c - 55).ToString() : c.ToString()));

        return BigInteger.Parse(convertedNumber) % 97 == 1;
    }
}