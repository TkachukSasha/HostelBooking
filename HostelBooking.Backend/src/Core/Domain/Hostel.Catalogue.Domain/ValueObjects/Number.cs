using Hostel.Catalogue.Domain.ExceptionCodes;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Catalogue.Domain.ValueObjects
{
    public sealed class Number
    {
        public int Value { get; }

        public Number(int value)
        {
            if (value is < 0 or > 600)
                throw new HostelException(Codes.InvalidNumber,
                    $"Number: {value} is invalid!");

            Value = value;
        }

        public static implicit operator Number(int value) => value is 0 ? 0 : new Number(value);
        public static implicit operator int(Number value) => value.Value;

        public override bool Equals(object obj) => obj is Number && Equals((Number)obj);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
