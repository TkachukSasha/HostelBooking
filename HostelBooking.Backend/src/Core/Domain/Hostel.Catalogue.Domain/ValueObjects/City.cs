using Hostel.Catalogue.Domain.ExceptionCodes;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Catalogue.Domain.ValueObjects
{
    public sealed class City
    {
        public string Value { get; }

        public City(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new HostelException(Codes.InvalidCity,
                    $"City: {value} is invalid!");

            Value = value;
        }

        public static implicit operator City(string value) => value is null ? null : new City(value);
        public static implicit operator string(City value) => value.Value;

        public override bool Equals(object obj) => obj is Name && Equals((Name)obj);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
