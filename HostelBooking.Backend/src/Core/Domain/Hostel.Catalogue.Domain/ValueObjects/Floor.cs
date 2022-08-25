using Hostel.Catalogue.Domain.ExceptionCodes;
using Hostel.Shared.Types.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hostel.Catalogue.Domain.ValueObjects
{
    public sealed class Floor
    {
        public int Value { get; }

        public Floor(int value)
        {
            if (value is < 0 or > 4)
                throw new HostelException(Codes.InvalidFloor,
                    $"Floor: {value} is invalid!");

            Value = value;
        }

        public static implicit operator Floor(int value) => new Floor(value);
        public static implicit operator int(Floor value) => value.Value;

        public override bool Equals(object obj) => obj is Floor && Equals((Floor)obj);
        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}
