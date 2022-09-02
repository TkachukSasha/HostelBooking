using Hostel.Catalogue.Domain.ExceptionCodes;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Catalogue.Domain.ValueObjects
{
    public sealed class Capacity
    {
        public int Value { get; }

        public Capacity(int value)
        {
            if (value is < 0 or > 4)
                throw new HostelException(Codes.InvalidFloor,
                    $"Capacity: {value} is invalid!");

            Value = value;
        }

        public static implicit operator Capacity(int value) => value is 0 ? 0 : new Capacity(value);
        public static implicit operator int(Capacity value) => value.Value;

        public override bool Equals(object obj) => obj is Capacity && Equals((Capacity)obj);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
