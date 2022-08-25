using Hostel.Catalogue.Domain.ExceptionCodes;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Catalogue.Domain.ValueObjects
{
    public sealed class Name
    {
        public string Value { get; }

        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new HostelException(Codes.InvalidName,
                    $"Name: {value} is invalid!");

            Value = value;
        }

        public static implicit operator Name(string value) => value is null ? null : new Name(value);
        public static implicit operator string(Name value) => value.Value;

        public override bool Equals(object obj) => obj is Name && Equals((Name)obj);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
