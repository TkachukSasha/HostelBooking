using Hostel.Security.Domain.ExceptionCodes;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Security.Domain.ValueObjects
{
    public sealed class Password
    {
        public string Value { get; }

        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new HostelException(Codes.InvalidPassword,
                    $"Passsword: {value} is invalid!");

            if (value.Length is < 8 or > 200)
                throw new HostelException(Codes.InvalidPassword,
                    $"Passsword: {value} is invalid!");

            Value = value;
        }

        public static implicit operator Password(string value) => value is null ? null : new Password(value);
        public static implicit operator string(Password value) => value.Value;

        public override bool Equals(object obj) => obj is Password && Equals((Password)obj);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
