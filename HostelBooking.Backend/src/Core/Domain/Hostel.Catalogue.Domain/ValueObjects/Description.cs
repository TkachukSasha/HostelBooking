using Hostel.Catalogue.Domain.ExceptionCodes;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Catalogue.Domain.ValueObjects
{
    public sealed class Description
    {
        public string Value { get; }

        public Description(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new HostelException(Codes.InvalidDescription,
                    $"Description: {value} is invalid!");

            Value = value;
        }

        public static implicit operator Description(string value) => value is null ? null : new Description(value);
        public static implicit operator string(Description value) => value.Value;

        public override bool Equals(object obj) => obj is Description && Equals((Description)obj);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value;
    }
}
