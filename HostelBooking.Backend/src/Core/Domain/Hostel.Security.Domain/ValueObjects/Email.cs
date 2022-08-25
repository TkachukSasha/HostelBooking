using Hostel.Security.Domain.Const;
using Hostel.Security.Domain.ExceptionCodes;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Security.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; set; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new HostelException(Codes.InvalidEmail,
                    $"Email: {value} is invalid!");

            if (value.Length > 100)
                throw new HostelException(Codes.InvalidEmail,
                   $"Email: {value} is invalid!");

            value = value.ToLowerInvariant();
            if (!StringConst.EmailTemplate.IsMatch(value))
                throw new HostelException(Codes.InvalidEmail,
                   $"Email: {value} is invalid!");

            Value = value;
        }

        public static implicit operator Email(string value) => value is null ? null : new Email(value);
        public static implicit operator string(Email value) => value.Value;

        public override bool Equals(object obj) => obj is Email && Equals((Email)obj);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
